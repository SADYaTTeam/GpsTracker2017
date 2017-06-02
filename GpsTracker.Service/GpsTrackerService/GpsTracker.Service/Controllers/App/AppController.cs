// <copyright file="AppController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

namespace GpsTracker.Service.Controllers.App
{
    #region using...
    using System;
    using System.Diagnostics;
    using System.Web.Http;
    using System.Linq;
    using Models.DataContext.Contexts;
    using Models.Messages;
    using Strategies.Base;
    using Strategies.ConcreateAppStrategies;
    #endregion

    /// <summary>
    /// Class representes web api 2 controller for app (path "../api/app")
    /// </summary>
    [RoutePrefix("api/app")]
    public class AppController : ApiController
    {
        /// <summary>
        /// Take marker message and proccess it(write info to write table, etc.)
        /// </summary>
        /// <param name="message">Marker message from mobile app</param>
        /// <returns>Return Ok(200) if message successfully been treated. And returns InternalServerError(500) if there's an exception while proccess</returns>
        [HttpPost]
        [Route("")]
        public ResultMessage TakeGeo([FromBody] GeoMessage message)
        {
            Strategy strategy;
            switch (message.Type)
            {
                case MessageType.Marker:
                    {
                        strategy = new MarkerStrategy();
                        break;
                    }
                case MessageType.Sos:
                    {
                        strategy = new SosStrategy();
                        break;
                    }
                default:
                    {
                        strategy = new UnknownTypeStrategy();
                        break;
                    }
            }
            return strategy.Execute(message);
        }

        /// <summary>
        /// Check db for the existance user with this deviceId
        /// </summary>
        /// <param name="message">Check message from mobile app</param>
        /// <returns>Returns Success(0) if there're user in db with that deviceId, 
        /// Decline(1) if not and UnknownError(-1) if there's an exception while process</returns>
        [HttpPost]
        [Route("check")]
        public ResultMessage CheckExist([FromBody] CheckMessage message)
        {
            try
            {
                if (message.DeviceId.Length != 16)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "Device id != 16"
                    };
                }
                if (MainContext.Instance.User.GetBy(x => message.DeviceId == x.DeviceId) != null)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Success,
                        Message = "There's an user in db with this deviceId"
                    };
                }
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "There's no user in db with this deviceId"
                };

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.UnknownError,
                    Message = "Internal server error " +
                              $"{ex.Message}"
                };
            }
        }

        /// <summary>
        /// Registrate new user in system
        /// </summary>
        /// <param name="message">Registration message from mobile app</param>
        /// <returns>Returns Success(1) if new user been successfully inserted, 
        /// Decline(1) if there're already user in db with this info or
        /// some fields in message are empty, 
        /// UnknownError(-1) if there's an exception while process </returns>
        [HttpPost]
        [Route("reg")]
        public ResultMessage Registration([FromBody] LoginMessage message)
        {
            try
            {
                if (message.GetType().GetProperties().Where(pi => pi.GetValue(message) is string)
                    .Select(pi => (string) pi.GetValue(message))
                    .Any(string.IsNullOrEmpty))
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "Some fields are empty"
                    };
                }
                if (MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId) != null)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "There's an user in db with this deviceId"
                    };
                }
                try
                {
                    if (MainContext.Instance.User.Insert(new Models.Models.User()
                    {
                        DeviceId = message.DeviceId,
                        IsAdmin = false,
                        Login = message.Login,
                        Password = message.Password
                    }))
                    {
                        var users = MainContext.Instance.User.GetAll().LastOrDefault();
                        var index = 1;
                        if (users != null)
                        {
                            index = users.UserId;
                        }
                        if (MainContext.Instance.Person.Insert(new Models.Models.Person()
                        {
                            UserId = index
                        }))
                        {
                            return new ResultMessage()
                            {
                                Type = ResultType.Success,
                                Message = "User successfully added to db"
                            };
                        }
                        return new ResultMessage()
                        {
                            Type = ResultType.Decline,
                            Message = "User been created, but can't create personal info entity." +
                                      "Create it through website(http://notbadtracker.azurewebsites.net/)"
                        };
                    }
                    else
                    {
                        return new ResultMessage()
                        {
                            Type = ResultType.Decline,
                            Message = "Can't add new user with this data"
                        };
                    }


                }
                catch (Exception)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "Input user information does not meet the requirements"
                    }; ;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.UnknownError,
                    Message = "Internal server error " +
                              $"{ex.Message}"
                };
            }
        }
    }
}
