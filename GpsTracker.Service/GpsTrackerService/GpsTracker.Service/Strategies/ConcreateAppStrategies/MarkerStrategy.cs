// <copyright file="MarkerStrategy.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    #region using...
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Diagnostics;
    using Models.Messages;
    using Models.Models;
    using Base;
    using Controllers;
    using Models.DataContext.Contexts;
    using Controllers.App;
    #endregion

    /// <summary>
    /// Class presents strategy for merker-type messages
    /// </summary>
    public class MarkerStrategy : Strategy
    {
        #region Fields
        // DELETE AFTER DELETING TRACK!!!
        private User _user;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="MarkerStrategy"/> class
        /// </summary>
        public MarkerStrategy()
        {
            
        }

        /// <summary>
        /// Initialized a new instance of <see cref="MarkerStrategy"/> class
        /// </summary>
        /// <param name="controller">App contoller instance</param>
        public MarkerStrategy(AppController controller) : base(controller)
        {
            
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Treating incoming marker-type message
        /// </summary>
        /// <param name="message">Marker-type message</param>
        /// <returns>Returns Ok(200) if process was successful and 
        /// InternalServerError(500) if there're is some server exception</returns>
        public override IHttpActionResult Execute(GeoMessage message)
        {
            try
            {
                _user = GetOrCreateUser(message);
                var temp = StaticInfo.MarkerList.FirstOrDefault(x => x.UserId == _user.UserId);
                if (temp == null)
                {
                    StaticInfo.MarkerList.Add(new Marker()
                    {
                        Latitude = message.Latitude,
                        Longtitude = message.Longitude,
                        Timestamp = DateTime.Now,
                        UserId = _user.UserId
                    });
                }
                else
                {
                    temp.Longtitude = message.Longitude;
                    temp.Latitude = message.Latitude;
                    temp.Timestamp = DateTime.Now;
                }
                WriteToDb(message);
                return new System.Web.Http.Results.OkResult(Controller);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in SosStrategy.Execute: {ex.Message}");
                return new System.Web.Http.Results.InternalServerErrorResult(Controller);
            }
        }

        /// <summary>
        /// Write new marker to db
        /// </summary>
        /// <param name="message">Inforamtion ablit new marker</param>
        protected override void WriteToDb(GeoMessage message)
        {
            try
            {
                var temp = (TrackContext)MainContext.Instance.Track;
                temp.Insert(_user, new Marker()
                {
                    Longtitude = message.Longitude,
                    Latitude = message.Latitude,
                    UserId = _user.UserId
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal Server exception: {ex.Message}");
            }
        }

        #endregion
    }
}