// <copyright file="SosStrategy.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    #region using...
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Web.Http;
    using Base;
    using Models.Messages;
    using Models.Models;    
    using Controllers;
    using Models.DataContext.Contexts;
    using Controllers.App;
    #endregion

    /// <summary>
    /// Represents strategy to treate sos-type messages
    /// </summary>
    public class SosStrategy : Strategy
    {
        #region Fields

        #endregion

        #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SosStrategy"/> class
        ///// </summary>
        //public SosStrategy()
        //{
            
        //}

        ///// <summary>
        ///// Initialized a new instance of the <see cref="SosStrategy"/> class
        ///// </summary>
        ///// <param name="controller">App contoller instance</param>
        //public SosStrategy(AppController controller) : base(controller)
        //{

        //}

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Treating incoming sos-type message
        /// </summary>
        /// <param name="message">Sos-type message</param>
        /// <returns>Returns Success(0) if process was successful and 
        /// InternalServerError(-1) if there're is some server exception</returns>
        public override ResultMessage Execute(GeoMessage message)
        {
            try
            {
                var index = GetOrCreateUser(message).UserId;
                var temp = StaticInfo.SosList.FirstOrDefault(x => x.UserId == index);
                if (temp == null)
                {
                    StaticInfo.SosList.Add(new SosMessage()
                    {
                        Latitude = message.Latitude,
                        Longitude = message.Longitude,
                        UserId = index,
                        Timestamp = DateTime.Now
                    });
                }
                else
                {
                    temp.Timestamp = DateTime.Now;
                }
                WriteToDb(message);
                return new ResultMessage()
                {
                    Type = ResultType.Success,
                    Message = "Server succesfully read incoming sos message"
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in SosStrategy.Execute: {ex.Message}");
                return new ResultMessage()
                {
                    Type = ResultType.UnknownError,
                    Message = "Internal server error " +
                              $"{ex.Message}"
                };
            }
        }

        /// <summary>
        /// Write to log message about sos call
        /// </summary>
        /// <param name="message">Information about sos call</param>
        protected override void WriteToDb(GeoMessage message)
        {
            try
            {
                MainContext.Instance.Log.Insert(new Log()
                {
                    DeviceId = message.DeviceId,
                    EventDate = DateTime.Now,
                    EventId = MainContext.Instance.Event.GetBy(x => x.Name == "SOS_BUTTON_CLICK").ToList()[0].EventId,
                    Message = $"Sos call on the mark: {message.Latitude}, {message.Longitude}"
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Internal DB Exception:{ex.Message}");
            }
        }

        #endregion
    }
}