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
    /// Class represents web api 2 controller for website (path ".../api/web")
    /// </summary>
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
        public ResultMessage LogInUser([FromBody] LoginMessage message)
        {
            try
            {
                if (string.IsNullOrEmpty(message.Login) && string.IsNullOrEmpty((message.Password)))
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "Login or password is empty."
                    };
                }
                var user = MainContext.Instance.User.GetBy(x => x.Login == message.Login &&
                                                                x.Password == message.Password);
                if (user == null)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "There's no user with this login and password."
                    };
                }
                return new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = "User successfully log in."
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.UnknownError,
                    Message = $"Internal server exception: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Edit person info of user with this login
        /// </summary>
        /// <param name="message">Message, that contain user login</param>
        /// <param name="newInfo">New person info about user</param>
        /// <returns>Returns Success(0) if process succesfully ended,
        /// Decline(1) if login null or empty,
        /// UnknownError(-1) if there're some exceptions while process</returns>
        [HttpPost]
        [Route("edit")]
        public ResultMessage EditPerson([FromBody] Person newInfo)
        {
            try
            {
                var user = MainContext.Instance.User.GetBy(x => x.UserId == newInfo.UserId).FirstOrDefault();
                if (user == null)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "There're no users with this login"
                    };
                }
                var person = MainContext.Instance.Person.GetBy(x => x.UserId == user.UserId).FirstOrDefault();
                if (person == null)
                {
                    throw new Exception("There's no person data about this user in db.");
                }
                if (MainContext.Instance.Person.Update(person.PersonId, newInfo))
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Success,
                        Message = "Person data successfully edited."
                    };
                }
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "Can't edit person info with this info" +
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
    }
}
