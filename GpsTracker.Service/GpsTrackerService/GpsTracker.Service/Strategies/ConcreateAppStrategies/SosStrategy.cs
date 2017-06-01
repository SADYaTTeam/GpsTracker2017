using GpsTracker.Service.Strategies.Base;
using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Web;
using GpsTracker.Models.Messages;
using GpsTracker.Models.Models;
using System.Web.Http;
using GpsTracker.Service.Controllers;
using GpsTracker.Models.DataContext.Contexts;
using System.Diagnostics;
using GpsTracker.Service.Controllers.App;

namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    public class SosStrategy : Strategy
    {
        #region Constructors

        public SosStrategy()
        { }

        public SosStrategy(AppController controller) : base(controller) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public override IHttpActionResult Execute(GeoMessage message)
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
                return new System.Web.Http.Results.OkResult(_controller);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in SosStrategy.Execute: {ex.Message}");
                return new System.Web.Http.Results.InternalServerErrorResult(_controller);
            }
        }

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