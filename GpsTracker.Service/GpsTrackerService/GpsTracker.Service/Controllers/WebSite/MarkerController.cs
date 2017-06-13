﻿// <copyright file="MarkerController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Controllers.WebSite
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Models.Models;
    using Models.Messages;
    using Models.DataContext.Contexts;
    using Models.Mappers;
    #endregion


    /// <summary>
    /// Class represents web api 2 controller for website(path ".../api/web/marker")
    /// </summary>
    [RoutePrefix("api/web/marker")]
    public class MarkerController : ApiController
    {
        /// <summary>
        /// Return available markers for current user
        /// </summary>
        /// <param name="message">UserId</param>
        /// <returns>Returns list of available markers for user or null if there're no markers 
        /// for this user</returns>
        [HttpPost]
        [Route("")]
        public List<Marker> ReturnByUserId([FromBody] CheckMessage message)
        {
            if (message?.UserId == null) return null;
            try
            {
                var result = new List<Marker>();
                var context = (FriendlistContext) MainContext.Instance.Friendlist;
                var friends = context.GetFriendOfUser((int) message.UserId)?.ToList();
                if (friends != null && friends.Count != 0)
                {
                    friends.Add((int) message.UserId);
                    result.AddRange(StaticInfo.MarkerList.Where(x => friends.Contains(x.UserId)).ToList());
                }
                if (result.Any()) return result;
                var markers = MainContext.Instance.Marker.GetAll();
                if (markers == null)
                {
                    return null;
                }
                var groups = (from marker in markers
                    group marker by marker.UserId).ToList();
                if (groups.Count == 0)
                {
                    return null;
                }
                var keys = groups.Select(x => x.Key).Distinct().ToList();
                int temp;
                foreach (var key in keys)
                {
                    temp = groups.Single(x => x.Key == key).ToList().Max(x => x.MarkerId);
                    result.Add(MainContext.Instance.Marker.GetBy(x => x.MarkerId==temp)?.ToList()[0]);
                }
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