// <copyright file="PersonController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

using System;

namespace GpsTracker.Service.Controllers.WebSite
{
    #region using...
    using System.Linq;
    using System.Web.Http;
    using Models.Models;
    using Models.Messages;
    using Models.DataContext.Contexts;
    #endregion
    /// <summary>
    /// Class represents web api 2 controller for website(path ".../api/web/person"
    /// </summary>
    /// <remarks>Mostly work with person info</remarks>
    [RoutePrefix("api/web/person")]
    public class PersonController : ApiController
    {
        /// <summary>
        /// Message returns person info about user
        /// </summary>
        /// <param name="message">UserId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public Person GetPersonByUserId([FromBody] CheckMessage message)
        {
            return MainContext.Instance.Person.GetBy(x => x.UserId == message.UserId)?.ToList()[0];
        }


        /// <summary>
        /// Edit person info of user with this login
        /// </summary>
        /// <param name="newInfo">New person info about user</param>
        /// <returns>Returns Success(0) if process succesfully ended,
        /// Decline(1) if there're no user with input userInfo
        /// UnknownError(-1) if there're some exceptions while process</returns>
        [HttpPost]
        [Route("edit")]
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
                        Message = "There're no users in db."
                    };
                }
                var user = users.FirstOrDefault();
                var person = MainContext.Instance.Person.GetBy(x => x.UserId == user.UserId).ToList()[0];
                //if (person == newInfo)
                //{
                //    return new ResultMessage()
                //    {
                //        Type = ResultType.Decline,
                //        Message = "You didn't change info."
                //    };
                //}
                if (MainContext.Instance.Person.Update(person.PersonId, newInfo))
                {
                    return new ResultMessage()
                    {
                        Type = ResultType.Success,
                        Message = "Personal info successfully edited."
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
