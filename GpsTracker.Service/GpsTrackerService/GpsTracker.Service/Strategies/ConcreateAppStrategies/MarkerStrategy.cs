using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GpsTracker.Models.Messages;
using GpsTracker.Models.Models;
using GpsTracker.Service.Strategies.Base;
using GpsTracker.Service.Controllers;
using GpsTracker.Models.DataContext.Contexts;
using System.Diagnostics;

namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    public class MarkerStrategy : Strategy
    {
        #region Constructors

        public MarkerStrategy(): base() { }

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

                return new System.Web.Http.Results.InternalServerErrorResult(_controller);
            }
            catch(Exception ex)
            {
                return new System.Web.Http.Results.InternalServerErrorResult(_controller);
            }
            //try
            //{
            //    var exist = MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId);
            //    //Async input to DB new user
            //    if (exist == null)
            //    {
            //        MainContext.Instance.User.Insert(new User()
            //        {
            //            DeviceId = message.DeviceId,
            //            IsAdmin = false,
            //            Login = message.DeviceId,
            //            Password = message.DeviceId
            //        });
            //    }
            //    user = exist.ToList()[0];
            //    var temp = StaticInfo.MarkerList.FirstOrDefault(x => x.UserId == user.UserId);
            //    if (temp == null)
            //    {
            //        StaticInfo.MarkerList.Add(new Marker()
            //        {
            //            UserId = user.UserId,
            //            Latitude = message.Latitutde,
            //            Longtitude = message.Longitude
            //        });
            //    }
            //    temp.Latitude = message.Latitutde;
            //    temp.Longtitude = message.Longitude;
            //    temp.Timestamp = DateTime.Now;
            //    WriteToDb(message); //Async
            //    return new System.Web.Http.Results.OkResult(_controller);
            //}
            //catch(Exception ex)
            //{
            //return new System.Web.Http.Results.InternalServerErrorResult(_controller);
            //}
        }

        protected override void WriteToDb(GeoMessage message)
        {
            //try
            //{
            //    MainContext.Instance.BeginTransaction();
            //    MainContext.Instance.Marker.Insert(new Marker()
            //    {
            //        Latitude = message.Latitutde,
            //        Longtitude = message.Longitude,
            //        UserId = user.UserId,
            //    });
            //    var temp = MainContext.Instance.Marker.GetAll().ToList();
            //    MainContext.Instance.Track.Insert(new Track()
            //    {
            //        UserId = user.UserId,
            //        MarkerId = (from item in temp
            //                    select item.MarkerId).Max() + 1
            //    });
            //}
            //catch(Exception ex)
            //{
            //    Debug.WriteLine($"Internal Server exception: {ex.Message}");
            //}
        }

        #endregion
    }
}