// <copyright file="MarkerStrategy.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    #region using...
    using System;
    using System.Linq;
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

        ///// <summary>
        ///// Initializes a new instance of<see cref= "MarkerStrategy" /> class
        ///// </summary>
        ////public MarkerStrategy()
        ////{

        ////}

        ///// <summary>
        ///// Initialized a new instance of <see cref="MarkerStrategy"/> class
        ///// </summary>
        ///// <param name="controller">App contoller instance</param>
        //public MarkerStrategy(AppController controller) : base(controller)
        //{

        //}

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
        public override ResultMessage Execute(GeoMessage message)
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
                return new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = "Server succesfully read incoming marker message"
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MarkerStrategy.Execute: {ex.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.UnknownError,
                    Message = "Internal server error " +
                              $"{ex.Message}"
                };
            }
        }

        /// <summary>
        /// Write new marker to db
        /// </summary>
        /// <param name="message">Inforamtion ablit new marker</param>
        protected override void WriteToDb(GeoMessage message)
        {
            MainContext.Instance.Marker.Insert(new Marker()
            {
                Latitude = message.Latitude,
                Longtitude = message.Longitude,
                UserId = _user.UserId
            });
        }

        #endregion
    }
}