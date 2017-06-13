// <copyright file="PersonController.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
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
    }
}
