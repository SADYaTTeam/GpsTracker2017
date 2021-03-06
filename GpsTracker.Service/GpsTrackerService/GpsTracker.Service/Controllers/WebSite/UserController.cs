﻿// <copyright file="UserController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

using System.Text.RegularExpressions;

namespace GpsTracker.Service.Controllers.WebSite
{
    #region using...
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Http;
    using System.Collections.Generic;
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
                //if (newInfo == user)
                //{
                //    return new ResultMessage()
                //    {
                //        Type = ResultType.Decline,
                //        Message = "You didn't change info."
                //    };
                //}
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

        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="message">Login of user</param>
        /// <returns></returns>
        [HttpPost]
        [Route("bylogin")]
        public User GetUserByLogin([FromBody] CheckMessage message)
        {
            if (string.IsNullOrEmpty(message?.Login))
            {
                return null;
            }
            try
            {
                return MainContext.Instance.User.GetBy(x => x.Login == message.Login)?.ToList()[0];
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in UserController.cs : {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Regex search by login
        /// </summary>
        /// <param name="message">Input string</param>
        /// <returns></returns>
        [HttpPost]
        [Route("regexp/login")]
        public List<User> RegexpByLogin([FromBody] CheckMessage message)
        {
            if (string.IsNullOrEmpty(message?.Login))
            {
                return null;
            }
            try
            {
                var rx = new Regex(message.Login+@".", RegexOptions.IgnoreCase);
                var logins = MainContext.Instance.User.GetAll()?.Select(x => x.Login).AsEnumerable();
                var result = logins?.Where(x => rx.IsMatch(x)).ToList();
                return MainContext.Instance.User.GetBy(x => result.Contains(x.Login))?.ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in UserController.cs : {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Regex search by deviceId
        /// </summary>
        /// <param name="message">Input string</param>
        /// <returns></returns>
        [HttpPost]
        [Route("regexp/deviceid")]
        public List<User> RegexpByDeviceId([FromBody] CheckMessage message)
        {
            if (string.IsNullOrEmpty(message?.DeviceId))
            {
                return null;
            }
            try
            {
                var rx = new Regex(message.DeviceId + @".", RegexOptions.IgnoreCase);
                var indexes = MainContext.Instance.User.GetAll()?.Select(x => x.DeviceId).AsEnumerable();
                var result = indexes?.Where(x => rx.IsMatch(x)).ToList();
                return MainContext.Instance.User.GetBy(x => result.Contains(x.DeviceId))?.ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in UserController.cs : {e.Message}");
                return null;
            }
        }


        /// <summary>
        /// Return user info by DeviceId
        /// </summary>
        /// <param name="indexes">List of device indexes</param>
        /// <returns>User info about user with this Device indexes</returns>
        [HttpPost]
        [Route("deviceId")]
        public List<CheckMessage> GetByUserId([FromBody] List<CheckMessage> indexes)
        {
            try
            {
                if (indexes?.Count > 0)
                {
                    var temp = indexes.Select(x => x.DeviceId);
                    return MainContext.Instance.User.GetBy(x => temp.Contains(x.DeviceId))?
                        .Select(x => new CheckMessage() {Login = x.Login, DeviceId = x.DeviceId}).ToList();
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in UserController.cs:{e.Message}");
                return null;
            }
        }
    }
}
