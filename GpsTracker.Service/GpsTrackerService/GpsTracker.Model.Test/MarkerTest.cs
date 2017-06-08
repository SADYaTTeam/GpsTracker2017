// <copyright file="MarkerTest.cs" company="SADYaTTeam">
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
    /// Class for test marker context
    /// </summary>
    [TestClass]
    public class MarkerTest
    {
        /// <summary>
        /// Method testes Insert(inserts test user and 
        /// test marker from him) method of MarkerContext
        /// </summary>
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
                Longitude = -20,
                UserId = index
            }));
        }

        /// <summary>
        /// Method testes GetAll method of MarkerContext
        /// </summary>
        [TestMethod]
        public void MarkerGetAll()
        {
            var result = MainContext.Instance.Marker.GetAll();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Method testes Update(set name of last marker "Test marker
        /// name" method of MarkerContext
        /// </summary>
        [TestMethod]
        public void MarkerUpdate()
        {
            var item = MainContext.Instance.Marker.GetAll().ToList().LastOrDefault();
            item.Name = "Test marker name";
            Assert.IsTrue(MainContext.Instance.Marker.Update(item.MarkerId, item));
        }

        /// <summary>
        /// Method testes GetBy(Name == "Test marker name") method of MarkerContext
        /// </summary>
        [TestMethod]
        public void MarkerGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.Marker.GetBy(x => x.Name == "Test marker name"));
        }

        /// <summary>
        /// Method testes Delete(delete last marker) method of MarkerContext
        /// </summary>
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
