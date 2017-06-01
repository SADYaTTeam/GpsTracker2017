// <copyright file="Strategy.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Strategies.Base
{
    #region using...
    using System.Web.Http;
    using System.Linq;
    using Models.Messages;
    using Models.Models;
    using Models.DataContext.Contexts;
    #endregion

    /// <summary>
    /// Class represents base class for strategies
    /// </summary>
    public abstract class Strategy
    {
        #region Fields

        /// <summary>
        /// Represents conntroller, that initialize strategy
        /// </summary>
        private readonly ApiController _controller;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Strategy"/> class
        /// </summary>
        protected Strategy()
        {
            
        }

        /// <summary>
        /// Initialized a new instance of <see cref="Strategy"/> class
        /// </summary>
        /// <param name="controller">App contoller instance</param>
        protected Strategy(ApiController controller)
        {
            _controller = controller;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets private api controller 
        /// </summary>
        protected ApiController Controller => _controller;

        #endregion

        #region Methods

        /// <summary>
        /// Virtual method for treating incoming marker message
        /// </summary>
        /// <param name="message">Marker message</param>
        /// <returns>Allways return InternalServerError(500) in base strategy class</returns>
        public virtual IHttpActionResult Execute(GeoMessage message)
        {
            return new System.Web.Http.Results.InternalServerErrorResult(_controller);
        }

        /// <summary>
        /// Virtual method for writing to db information
        /// </summary>
        /// <param name="message"></param>
        protected virtual void WriteToDb(GeoMessage message)
        {
            
        }

        /// <summary>
        /// Gets user with deviceId from db
        /// </summary>
        /// <param name="message">Marker message</param>
        /// <returns>Return info of user, that have deviceId from message. 
        /// If There're no users with that deviceId - insert new with login,
        /// password and equal to deviceId and return information about him</returns>
        protected virtual User GetOrCreateUser(GeoMessage message)
        {

            var exist = MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId);
            if (exist == null)
            {
                MainContext.Instance.User.Insert(new User()
                {
                    DeviceId = message.DeviceId,
                    IsAdmin = false,
                    Login = message.DeviceId,
                    Password = message.DeviceId
                });
                exist = MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId);
                MainContext.Instance.Person.Insert(new Person()
                {
                    UserId = exist.ToList()[0].UserId
                });
            }
            return exist.ToList()[0];
        }

        #endregion
    }
}