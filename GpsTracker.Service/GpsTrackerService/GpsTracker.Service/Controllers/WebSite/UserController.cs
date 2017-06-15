// <copyright file="UserController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Controllers.WebSite
{
    #region using...
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Http;
    using Models.DataContext.Contexts;
    using Models.Messages;
    using Models.Models;
    #endregion

    /// <summary>
    /// Class represents web api 2 controller for website (path ".../api/web/user")
    /// </summary>
    /// <remarks>Mostly work with user info</remarks>
    [RoutePrefix("api/web/user")]
    public class UserController : ApiController
    {
        /// <summary>
        /// Method checks if there's an user with this login and password
        /// </summary>
        /// <param name="message">Message, that contain login and password</param>
        /// <returns>Returns Success(0) if there's an user in db with this login and
        /// password, Decline(1) if not and UnknownError(-1) if there're some
        /// exceptions while process</returns>
        [HttpPost]
        [Route("login")]
        public User LogInUser([FromBody]  LoginMessage message)
        {
            try
            {
                if (string.IsNullOrEmpty(message.Login) && string.IsNullOrEmpty((message.Password)))
                {
                    return null;
                }
                var user = MainContext.Instance.User.GetBy(x => x.Login == message.Login &&
                                                                x.Password == message.Password);
                return user?.ToList()[0];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Edit user info of user with this login
        /// </summary>
        /// <param name="newInfo">New user info about user</param>
        /// <returns>Returns Success(0) if process succesfully ended,
        /// Decline(1) if there're no user with input userInfo,
        /// UnknownError(-1) if there're some exceptions while process</returns>
        [HttpPost]
        [Route("edit")]
        public ResultMessage EditUser([FromBody] User newInfo)
        {
            try
            {
                var users = MainContext.Instance.User.GetBy(x => x.UserId == newInfo.UserId);
                if (users == null)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "There's no user in DB."
                    };
                }
                var user = users.FirstOrDefault();
                if (newInfo == user)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "You didn't change info."
                    };
                }
                if (MainContext.Instance.User.Update(user.UserId, newInfo))
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Success,
                        Message = "User info successfully edited."
                    };
                }
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "Can't edit user info with this info" +
                              "(mb you write not accessible data)."
                };

            }
            catch (Exception ex)
            {
                return new ResultMessage()
                {
                    Type = ResultType.UnknownError,
                    Message = $"Internal server error: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Return user info by UserId
        /// </summary>
        /// <param name="message">UserId</param>
        /// <returns>User info about user with this UserId</returns>
        [HttpPost]
        [Route("id")]
        public User LogInOnId([FromBody] CheckMessage message)
        {
            try
            {
                return MainContext.Instance.User.GetBy(x => x.UserId == message.UserId)?.ToList()[0];
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in UserController.cs : {e.Message}");
                return null;
            }
        }
    }
}
