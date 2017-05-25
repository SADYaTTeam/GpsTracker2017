using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GpsTracker.Models.DataContext.Contexts;
using System.Linq;

namespace GpsTracker.Model.Test
{
    [TestClass]
    public class TrackTest
    {
        [TestMethod]
        public void TrackInsert()
        {
            MainContext.Instance.User.Insert(new Models.Models.User()
            {
                Login = "test login",
                Password = "test Password",
                DeviceId = "1234567812345678",
                IsAdmin = false
            });
            var userIndex = MainContext.Instance.User.GetAll().ToList()[0].UserId;
            MainContext.Instance.Marker.Insert(new Models.Models.Marker()
            {
                Latitude = -10,
                Longtitude = 20,
                UserId = userIndex
            });
            var markerIndex = MainContext.Instance.Marker.GetAll().ToList()[0].MarkerId;
            Assert.IsTrue(MainContext.Instance.Track.Insert(new Models.Models.Track()
            {
                UserId = userIndex,
                MarkerId = markerIndex
            }));
        }

        [TestMethod]
        public void TrackGetAll()
        {
            var result = MainContext.Instance.Track.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TrackUpdate()
        {
            MainContext.Instance.Marker.Insert(new Models.Models.Marker()
            {
                Latitude = -30,
                Longtitude = -50,
                UserId = MainContext.Instance.User.GetAll().ToList()[0].UserId
            });
            var markerIndex = MainContext.Instance.Marker.GetBy(x => x.Latitude == -30).FirstOrDefault().MarkerId;
            var item = MainContext.Instance.Track.GetAll().ToList().LastOrDefault();
            item.MarkerId = markerIndex;
            Assert.IsTrue(MainContext.Instance.Track.Update(item.TrackId, item));
        }

        [TestMethod]
        public void TrackGetBy()
        {
            var markerIndex = MainContext.Instance.Marker.GetBy(x => x.Latitude == -30).FirstOrDefault().MarkerId;
            Assert.IsNotNull(MainContext.Instance.Marker.GetBy(x => x.MarkerId == markerIndex));
        }

        [TestMethod]
        public void TrackDelete()
        {
            var dataSet = MainContext.Instance.Track.GetAll().ToList();
            var index = (from item in dataSet
                         select item.TrackId).Max();
            Assert.IsTrue(MainContext.Instance.Track.Delete(index));
        }
    }
}
