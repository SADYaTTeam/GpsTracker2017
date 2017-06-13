// <copyright file="MaiPageController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Controllers.WebSite
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    //using System.Net;
    //using System.Net.Http;
    using System.Web.Http;
    using GpsTracker.Models.Models;
    using GpsTracker.Models.DataContext.Contexts;
    using GpsTracker.Models.Messages;
    using System.Diagnostics;
    #endregion
    /// <summary>
    /// Class represents web api 2 controller for website (path ".../api/web/main")
    /// </summary>
    /// <remarks>Mostly work with main</remarks>
    [RoutePrefix("api/web/main")]
    public class MainPageController : ApiController
    {
        /// <summary>
        /// Search users in db
        /// </summary>
        /// <param name="message">Search parameters</param>
        /// <returns>Return list of finded users or null, if can't find user</returns>
        [Route("search")]
        [HttpPost]
        public List<User> SearchInDb([FromBody]SearchMessage message)
        {
            if (string.IsNullOrEmpty(message.Value))
            {
                return null;
            }
            try
            {
                switch (message.Property)
                {
                    case SearchType.Id:
                    {
                        var index = Convert.ToInt32(message.Value);
                        return (List<User>) MainContext.Instance.User.GetBy(x => x.UserId == index);
                    }
                    case SearchType.Login:
                    {
                        return (List<User>)MainContext.Instance.User.GetBy(x => x.Login == message.Value);
                    }
                    default:
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Internal server exception" +
                                $"{ex.Message}");
                return null;
            }
        }

        [Route("all")]
        [HttpGet]
        public List<User> GetAll()
        {
            return (List<User>)MainContext.Instance.User.GetAll();
        }

    }
}
