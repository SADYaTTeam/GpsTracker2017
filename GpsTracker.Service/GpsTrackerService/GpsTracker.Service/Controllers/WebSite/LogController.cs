// <copyright file="LogController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Controllers.WebSite
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Http;
    using Models.Models;
    using Models.Messages;
    using Models.DataContext.Contexts;
    #endregion

    /// <summary>
    /// Class represents web api 2 controller for website(path ".../api/wbe/log")
    /// </summary>
    [RoutePrefix("api/web/log")]
    public class LogController : ApiController
    {
        /// <summary>
        /// Get user info log messages about friends of user
        /// </summary>
        /// <param name="message">Id of user</param>
        /// <returns></returns>
        [HttpPost]
        [Route("user")]
        public List<Log> GetUserLog([FromBody] CheckMessage message)
        {
            try
            { 
            if (message?.UserId == null) return null;
            var context = (FriendlistContext) MainContext.Instance.Friendlist;
            var friends = context.GetFriendOfUser((int)message.UserId).ToList();
            friends.Add((int)message.UserId);
            var deviceIndexes = MainContext.Instance.User.GetBy(x => friends.Contains(x.UserId))
                                .Select(x => x.DeviceId).ToList();
            var eventIndexes = MainContext.Instance.Event
                .GetBy(x => x.Name == "NEW_USER" || x.Name == "EDIT_USER" || x.Name == "DELETE_USER")?
                .Select(x => x.EventId).ToList();
            if (eventIndexes == null)
            {
                return null;
            }
            return MainContext.Instance.Log
                .GetBy(x => deviceIndexes.Contains(x.DeviceId) && eventIndexes.Contains(x.EventId))?.OrderByDescending(x=>x.LogId).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception in LogController.cs: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get sos log messages about friends of user
        /// </summary>
        /// <param name="message">Id of user</param>
        /// <returns></returns>
        [HttpPost]
        [Route("sos")]
        public List<Log> GetSosLog([FromBody] CheckMessage message)
        {
            if (message?.UserId == null) return null;
            try
            {
                var context = (FriendlistContext) MainContext.Instance.Friendlist;
                var friends = context.GetFriendOfUser((int) message.UserId).ToList();
                friends.Add((int) message.UserId);
                var deviceIndexes = MainContext.Instance.User.GetBy(x => friends.Contains(x.UserId))
                    .Select(x => x.DeviceId).ToList();
                var eventIndexes = MainContext.Instance.Event
                    .GetBy(x => x.Name == "SOS_BUTTON_CLICK")?
                    .Select(x => x.EventId).ToList();
                if (eventIndexes == null)
                {
                    return null;
                }
                return MainContext.Instance.Log
                    .GetBy(x => deviceIndexes.Contains(x.DeviceId) && eventIndexes.Contains(x.EventId))?.OrderByDescending(x => x.LogId).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception in LogController.cs: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get zones log messages about friends of user
        /// </summary>
        /// <param name="message">Id of user</param>
        /// <returns></returns>
        [HttpPost]
        [Route("zone")]
        public List<Log> GetZoneLog([FromBody] CheckMessage message)
        {
            if (message?.UserId == null) return null;
            try
            { 
            var context = (FriendlistContext)MainContext.Instance.Friendlist;
            var friends = context.GetFriendOfUser((int)message.UserId).ToList();
            friends.Add((int)message.UserId);
            var deviceIndexes = MainContext.Instance.User.GetBy(x => friends.Contains(x.UserId))
                .Select(x => x.DeviceId).ToList();
            var eventIndexes = MainContext.Instance.Event
                .GetBy(x => x.Name == "OUT_OF_SAFE_ZONE" || x.Name == "IN_SAFE_ZONE")?
                .Select(x => x.EventId).ToList();
            if (eventIndexes == null)
            {
                return null;
            }
            return MainContext.Instance.Log
                .GetBy(x => deviceIndexes.Contains(x.DeviceId) && eventIndexes.Contains(x.EventId))?.OrderByDescending(x => x.LogId).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server exception in LogController.cs: {ex.Message}");
                return null;
            }
        }
    }
}
