// <copyright file="SosController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Controllers.WebSite
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Models.Models;
    using Models.Mappers;
    using Models.Messages;
    using Models.DataContext.Contexts;
    #endregion
    /// <summary>
    /// Class represents web api 2 controller for website(path ".../api/web/sos")
    /// </summary>
    [RoutePrefix("api/web/sos")]
    public class SosController : ApiController
    {
        [HttpPost]
        [Route("")]
        public List<SosMessage> ReturnSos([FromBody] CheckMessage message)
        {
            if (message?.UserId == null) return null;
            try
            {
                var result = new List<SosMessage>();
                var context = (FriendlistContext)MainContext.Instance.Friendlist;
                var friends = context.GetFriendOfUser((int)message.UserId)?.ToList();
                var diff = new System.TimeSpan(0, 0, 15, 0);
                List<SosMessage> temp;
                if (friends != null && friends.Count != 0)
                {
                    friends.Add((int)message.UserId);
                    temp = StaticInfo.SosList.Where(x => friends.Contains(x.UserId) && DateTime.Now.TimeOfDay.Subtract(x.Timestamp.TimeOfDay) < diff).ToList();
                }
                else
                {
                    temp = StaticInfo.SosList.Where(x => x.UserId == message.UserId)?.ToList();
                }
                result.AddRange(temp);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in MarkerController.cs: {e.Message}");
                return null;
            }
        }
    }
}
