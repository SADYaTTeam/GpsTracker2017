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
    /// Class representes web api 2 controller for path "../api/app"
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
        public IHttpActionResult TakeGeo([FromBody] GeoMessage message)
        {
            Strategy strategy;
            switch (message.Type)
            {
                case MessageType.Marker:
                    {
                        strategy = new MarkerStrategy(this);
                        break;
                    }
                case MessageType.Sos:
                    {
                        strategy = new SosStrategy(this);
                        break;
                    }
                default:
                    {
                        strategy = new UnknownTypeStrategy(this);
                        break;
                    }
            }
            return strategy.Execute(message);
        }

        /// <summary>
        /// Check db for the existance user with this deviceId
        /// </summary>
        /// <param name="message">Check message from mobile app</param>
        /// <returns>Returns Ok(200) if there're user in db with that deviceId, BadRequest(400) if not and InternalServerError(500) if there's an exception while process</returns>
        [HttpPost]
        [Route("check")]
        public IHttpActionResult CheckExist([FromBody] CheckMessage message)
        {
            try
            {
                if (message.DeviceId.Length != 16)
                {
                    throw new Exception("DeviceId length != 16");
                }
                if (MainContext.Instance.User.GetBy(x => message.DeviceId == x.DeviceId) != null)
                {
                    return new System.Web.Http.Results.OkResult(this);
                }
                return new System.Web.Http.Results.BadRequestResult(this);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return new System.Web.Http.Results.InternalServerErrorResult(this);
            }
        }

        /// <summary>
        /// Registrate new user in system
        /// </summary>
        /// <param name="message">Registration message from mobile app</param>
        /// <returns>Returns Ok(200) if new user been successfully inserted, BadRequest(400) if there're already user in db with this info or
        /// some fields in message are empty, InternalServerError(500) if there's an exception while process </returns>
        [HttpPost]
        [Route("reg")]
        public IHttpActionResult Registration([FromBody] RegistrationMessage message)
        {
            try
            {
                if (message.GetType().GetProperties().Where(pi => pi.GetValue(message) is string)
                    .Select(pi => (string) pi.GetValue(message))
                    .Any(String.IsNullOrEmpty))
                {
                    return new System.Web.Http.Results.BadRequestResult(this);
                }
                if (MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId) != null)
                {
                    return new System.Web.Http.Results.BadRequestResult(this);
                }
                try
                {
                    MainContext.Instance.BeginTransaction();
                    MainContext.Instance.User.Insert(new Models.Models.User()
                    {
                        DeviceId = message.DeviceId,
                        IsAdmin = false,
                        Login = message.DeviceId,
                        Password = message.DeviceId
                    });
                    var user = MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId).FirstOrDefault();
                    MainContext.Instance.Person.Insert(new Models.Models.Person()
                    {
                        UserId = user.UserId
                    });
                    MainContext.Instance.CommitTransaction();
                    return new System.Web.Http.Results.OkResult(this);
                }
                catch (Exception)
                {
                    MainContext.Instance.RollbackTransaction();
                    return new System.Web.Http.Results.BadRequestResult(this);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return new System.Web.Http.Results.InternalServerErrorResult(this);
            }
        }
    }
}
