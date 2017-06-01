// <copyright file="TrackTest.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Model.Test
{
    #region using...
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.DataContext.Contexts;
    #endregion

    /// <summary>
    /// Class for test track context
    /// </summary>
    [TestClass]
    public class TrackTest
    {
        /// <summary>
        /// Method testes Insert(insert temp user, marker and track with them)
        /// method of TrackContext
        /// </summary>
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

        /// <summary>
        /// Method testes GetAll method of TrackContext
        /// </summary>
        [TestMethod]
        public void TrackGetAll()
        {
            var result = MainContext.Instance.Track.GetAll();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Method testes Update method of TrackContext
        /// </summary>
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

        /// <summary>
        /// Method testes GetBy method of TrackContext
        /// </summary>
        [TestMethod]
        public void TrackGetBy()
        {
            var markerIndex = MainContext.Instance.Marker.GetBy(x => x.Latitude == -30).FirstOrDefault().MarkerId;
            Assert.IsNotNull(MainContext.Instance.Marker.GetBy(x => x.MarkerId == markerIndex));
        }

        /// <summary>
        /// Method testes Delete(delete last inserted track) method of TrackContext
        /// </summary>
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
