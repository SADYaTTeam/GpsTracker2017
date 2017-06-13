﻿// <copyright file="UserController.cs" company="SADYaTTeam">
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
                    //return new ResultMessage()
                    //{
                    //    Type = ResultType.Decline,
                    //    Message = "Login or password is empty."
                    //};
                }
                var user = MainContext.Instance.User.GetBy(x => x.Login == message.Login &&
                                                                x.Password == message.Password);
                if (user == null)
                {
                    return null;
                    //return new ResultMessage()
                    //{
                    //    Type = ResultType.Decline,
                    //    Message = "There's no user with this login and password."
                    //};
                }
                return user.ToList()[0];
                //return new ResultMessage()
                //{
                //    Type = ResultType.Success,
                //    Message = "User successfully log in."
                //};
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return null;
                //return new ResultMessage()
                //{
                //    Type = ResultType.UnknownError,
                //    Message = $"Internal server exception: {ex.Message}"
                //};
            }
        }

        /// <summary>
        /// Edit person info of user with this login
        /// </summary>
        /// <param name="newInfo">New person info about user</param>
        /// <returns>Returns Success(0) if process succesfully ended,
        /// Decline(1) if there're no user with input userInfo
        /// UnknownError(-1) if there're some exceptions while process</returns>
        [HttpPost]
        [Route("editPerson")]
        public ResultMessage EditPerson([FromBody] Person newInfo)
        {
            try
            {
                var users = MainContext.Instance.User.GetBy(x => x.UserId == newInfo.UserId);
                if (users == null)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "There're no users with this login"
                    };
                }
                var user = users.FirstOrDefault();
                var person = MainContext.Instance.Person.GetBy(x => x.UserId == user.UserId);
                if (person == null)
                {
                    throw new Exception("There's no person data about this user in db." +
                                        "Create it through website(http://notbadtracker.azurewebsites.net/)");
                }
                if (MainContext.Instance.Person.Update(person.ToList()[0].PersonId, newInfo))
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

        /// <summary>
        /// Edit user info of user with this login
        /// </summary>
        /// <param name="newInfo">New user info about user</param>
        /// <returns>Returns Success(0) if process succesfully ended,
        /// Decline(1) if there're no user with input userInfo,
        /// UnknownError(-1) if there're some exceptions while process</returns>
        [HttpPost]
        [Route("editUser")]
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
                        Message = "There're no users with this login"
                    };
                }
                var user = users.FirstOrDefault();
                if (MainContext.Instance.User.Update(user.UserId, newInfo))
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
