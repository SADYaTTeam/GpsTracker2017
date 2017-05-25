using GpsTracker.Service.Strategies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GpsTracker.Models.Models;
using System.Web.Http;
using GpsTracker.Service.Controllers;
using GpsTracker.Models.DataContext.Contexts;
using System.Diagnostics;

namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    public class SosStrategy : Strategy
    {
        #region Constructors

        public SosStrategy() : base() { }

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
                var temp = StaticInfo.SosList.FirstOrDefault(x => x.DeviceId == message.DeviceId);
                if (temp == null)
                {
                    StaticInfo.SosList.Add(new Log()
                    {
                        DeviceId = message.DeviceId,
                        EventDate = DateTime.Now,
                        EventId = MainContext.Instance.Event.GetBy(x => x.Name == "SOS_BUTTON_CLICK").ToList()[0].EventId
                    });
                }
                temp.EventDate = DateTime.Now;
                WriteToDb(message); //Async
                return new System.Web.Http.Results.OkResult(_controller);
            }
            catch(Exception ex)
            {
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
                    EventId = MainContext.Instance.Event.GetBy(x => x.Name == "SOS_BUTTON_CLICK").ToList()[0].EventId
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Internal DB Exception:{ex.Message}");
            }
        }

        #endregion
    }
}