using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GpsTracker.Models.DataContext.Contexts;
using System.Linq;

namespace GpsTracker.Model.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void UserInsert()
        {
            Assert.IsTrue(MainContext.Instance.User.Insert(new Models.Models.User()
            {
                Login = "Test User",
                Password = "Test Password",
                DeviceId = "1234567812345678",
                IsAdmin = false
            }));
        }

        [TestMethod]
        public void UserGetAll()
        {
            var result = MainContext.Instance.User.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UserUpdate()
        {
            var item = MainContext.Instance.User.GetAll().ToList().LastOrDefault();
            item.Login = "Updated Login";
            item.IsAdmin = true;
            item.DateCreatedAt = DateTime.Now;
            Assert.IsTrue(MainContext.Instance.User.Update(item.UserId, item));
        }

        [TestMethod]
        public void UserGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.User.GetBy(x => x.Login == "Updated Login"));
        }

        [TestMethod]
        public void UserDelete()
        {
            var dataSet = MainContext.Instance.User.GetAll().ToList();
            var index = (from item in dataSet
                         select item.UserId).Max();
            Assert.IsTrue(MainContext.Instance.User.Delete(index));
        }
    }
}
