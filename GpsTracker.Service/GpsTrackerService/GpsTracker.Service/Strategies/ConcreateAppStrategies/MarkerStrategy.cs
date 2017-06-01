using System;
using System.Linq;
using System.Web.Http;
using GpsTracker.Models.Messages;
using GpsTracker.Models.Models;
using GpsTracker.Service.Strategies.Base;
using GpsTracker.Service.Controllers;
using GpsTracker.Models.DataContext.Contexts;
using System.Diagnostics;
using GpsTracker.Service.Controllers.App;

namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    public class MarkerStrategy : Strategy
    {
        #region Constructors

        public MarkerStrategy()
        { }

        public MarkerStrategy(AppController controller): base(controller) { }

        #endregion

        #region Fields

        private User user;

        #endregion

        #region Properties

        #endregion

        #region Methods

        public override IHttpActionResult Execute(GeoMessage message)
        {
            try
            {
                user = GetOrCreateUser(message);
                var temp = StaticInfo.MarkerList.FirstOrDefault(x => x.UserId == user.UserId);
                if (temp == null)
                {
                    StaticInfo.MarkerList.Add(new Marker()
                    {
                        Latitude = message.Latitude,
                        Longtitude = message.Longitude,
                        Timestamp = DateTime.Now,
                        UserId = user.UserId
                    });
                }
                else
                {
                    temp.Longtitude = message.Longitude;
                    temp.Latitude = message.Latitude;
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
                TrackContext temp = (TrackContext)MainContext.Instance.Track;
                temp.Insert(user, new Marker()
                {
                    Longtitude = message.Longitude,
                    Latitude = message.Latitude,
                    UserId = user.UserId
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