using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GpsTracker.Models.DataContext.Contexts;
using System.Linq;

namespace GpsTracker.Model.Test
{
    [TestClass]
    public class MarkerTest
    {
        [TestMethod]
        public void MarkerInsert()
        {
            MainContext.Instance.User.Insert(new Models.Models.User()
            {
                Login = "Test login",
                Password = "Test password",
                DeviceId = "1234567812345678",
                IsAdmin = false
            });
            var index = MainContext.Instance.User.GetAll().ToList()[0].UserId;
            Assert.IsTrue(MainContext.Instance.Marker.Insert(new Models.Models.Marker()
            {
                Latitude = 20,
                Longtitude = -20,
                UserId = index
            }));
        }

        [TestMethod]
        public void MarkerGetAll()
        {
            var result = MainContext.Instance.Marker.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MarkerUpdate()
        {
            var item = MainContext.Instance.Marker.GetAll().ToList().LastOrDefault();
            item.Name = "Test marker name";
            Assert.IsTrue(MainContext.Instance.Marker.Update(item.MarkerId, item));
        }

        [TestMethod]
        public void MarkerGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.Marker.GetBy(x => x.Name == "Test marker name"));
        }

        [TestMethod]
        public void MarkerDelete()
        {
            var dataSet = MainContext.Instance.Marker.GetAll().ToList();
            var index = (from item in dataSet
                         select item.MarkerId).Max();
            Assert.IsTrue(MainContext.Instance.Marker.Delete(index));
        }
    }
}
