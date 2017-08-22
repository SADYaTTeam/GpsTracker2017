// <copyright file="UserTest.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Model.Test
{
    #region using...
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.DataContext.Contexts;
    #endregion

    /// <summary>
    /// Class for test user context
    /// </summary>
    [TestClass]
    public class UserTest
    {
        /// <summary>
        /// Method testes Insert(insert test user) method of UserContext
        /// </summary>
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

        /// <summary>
        /// Method testes GetAll method of UserContext
        /// </summary>
        [TestMethod]
        public void UserGetAll()
        {
            var result = MainContext.Instance.User.GetAll();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Method testes Update(set last inserted user admin status
        /// and login = "Updated Login") method of UserContext
        /// </summary>
        [TestMethod]
        public void UserUpdate()
        {
            var item = MainContext.Instance.User.GetAll().ToList().LastOrDefault();
            item.Login = "Updated Login";
            item.IsAdmin = true;
            item.DateCreatedAt = DateTime.Now;
            Assert.IsTrue(MainContext.Instance.User.Update(item.UserId, item));
        }

        /// <summary>
        /// Method testes GetBy(Login == "Updated Login") method of UserContext
        /// </summary>
        [TestMethod]
        public void UserGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.User.GetBy(x => x.Login == "Updated Login"));
        }

        /// <summary>
        /// Method testes Delete(delete last inserted user) method of UserContext
        /// </summary>
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
