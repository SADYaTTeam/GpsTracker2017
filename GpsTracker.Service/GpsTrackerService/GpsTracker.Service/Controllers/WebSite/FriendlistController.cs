// <copyright file="FriendlistController.cs" company="SADYaTTeam">
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
    /// Class represents web api 2 controller for website (path ".../api/web/friendlist")
    /// </summary>
    [RoutePrefix("api/web/friendlist")]
    public class FriendlistController : ApiController
    {
        /// <summary>
        /// Get friends of user
        /// </summary>
        /// <param name="message">User id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public List<User> GetFriendsBuUserId([FromBody] CheckMessage message)
        {
            if (message?.UserId == null) return null;
            try
            {
                var context = (FriendlistContext) MainContext.Instance.Friendlist;
                var friends = context.GetFriendOfUser((int) message.UserId);
                return MainContext.Instance.User.GetBy(x => friends.Contains(x.UserId))?.ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Internal server error: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get friend requests to user
        /// </summary>
        /// <param name="message">User id</param>
        [HttpPost]
        [Route("request")]
        public List<User> GetFriendlistRequests([FromBody] CheckMessage message)
        {
            if (message?.UserId == null) return null;
            try
            {
                var context = (FriendlistContext)MainContext.Instance.Friendlist;
                var indexes = context.GetRequests((int)message.UserId)?.ToList();
                return MainContext.Instance.User.GetBy(x => indexes.Contains(x.UserId))?.ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Internal server error: {e.Message}");
                return null;
            }
        }

        [HttpPost]
        [Route("add")]
        public ResultMessage SendRequset([FromBody] FriendsMessage message)
        {
            if (message?.UserId == null || message.FriendId == null) return new ResultMessage()
            {
                Type = ResultType.Decline,
                Message = "Wrong input data"
            };
            try
            {
                var result = MainContext.Instance.Friendlist.Insert(new Friendlist()
                {
                    Sender = (int)message.UserId,
                    Marked = (int)message.FriendId
                });
                if (result)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Success,
                        Message = "Request successfully sended"
                    };
                }
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "Can't send request to this user(mb you already send it)"
                };

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal server error in FriendlistController: {ex.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "Internal server error in FriendlistController" +
                              $": {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Accept incoming request
        /// </summary>
        /// <param name="message">Indexes of user and his new friend</param>
        /// <returns></returns>
        [HttpPost]
        [Route("accept")]
        public ResultMessage AcceptRequest([FromBody] FriendsMessage message)
        {
            if (message?.UserId == null || message.FriendId == null) return new ResultMessage()
            {
                Type = ResultType.Decline,
                Message = "Wrong input data"
            };
            try
            {
                var result = MainContext.Instance.Friendlist.Insert(new Friendlist()
                {
                    Sender = (int)message.UserId,
                    Marked = (int)message.FriendId
                });
                if (result)
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Success,
                        Message = "Friend successfully added"
                    };
                }
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "You are allready in relation"
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Internal server error in FriendlistController: {e.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "Internal server error in FriendlistController" +
                              $": {e.Message}"
                };
            }
        }

        /// <summary>
        /// Disconnect friends
        /// </summary>
        /// <param name="message">Indexes of user and his ex friend</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public ResultMessage DeleteFriend([FromBody] FriendsMessage message)
        {
            if (message?.UserId == null || message.FriendId == null) return new ResultMessage()
            {
                Type = ResultType.Decline,
                Message = "Wrong input data"
            };
            try
            {
                var indexes = new List<int>();
                var i = MainContext.Instance.Friendlist
                    .GetBy(x => x.Sender == message.UserId && x.Marked == message.FriendId)?.Select(x => x.ItemId)
                    .FirstOrDefault();
                if (i != null)
                {
                    indexes.Add((int) i);
                }
                else
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "Can't disconect friends with this data"
                    };
                }
                i = MainContext.Instance.Friendlist
                    .GetBy(x => x.Marked == message.UserId && x.Sender == message.FriendId)?.Select(x => x.ItemId)
                    .FirstOrDefault();
                if (i != null)
                {
                    indexes.Add((int) i);
                }
                else
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Decline,
                        Message = "Can't disconect friends with this data"
                    };
                }
                foreach (var item in indexes)
                {
                    if (!MainContext.Instance.Friendlist.Delete(item))
                    {
                        return new ResultMessage()
                        {
                            Type = ResultType.Decline,
                            Message = "Can't disconect friends with this data"
                        };
                    }
                }
                return new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = "Users successfully disconnected"
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Internal server error in FriendlistController: {e.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.Decline,
                    Message = "Internal server error in FriendlistController" +
                              $": {e.Message}"
                };
            }
        }
    }
}
