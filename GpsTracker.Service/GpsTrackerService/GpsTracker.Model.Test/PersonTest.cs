// <copyright file="PersonTest.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Model.Test
{
    #region using...
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.DataContext.Contexts;
    #endregion

    /// <summary>
    /// Class for test person context
    /// </summary>
    [TestClass]
    public class PersonTest
    {
        /// <summary>
        /// Method testes Insert(insert test user and test person
        /// with all fields) method of PersonContext
        /// </summary>
        [TestMethod]
        public void PersonInsert()
        {
            MainContext.Instance.User.Insert(new Models.Models.User()
            {
                Login = "Test user",
                Password = "Test Password",
                DeviceId = "1234567812345678",
                IsAdmin = false
            });
            var index = MainContext.Instance.User.GetAll().ToList()[0].UserId;
            using (var ms = new MemoryStream())
            {
                Image.FromFile("1.jpg").Save(ms, ImageFormat.Jpeg);
                Assert.IsTrue(MainContext.Instance.Person.Insert(new Models.Models.Person()
                {
                    UserId = index,
                    DateOfBirth = new DateTime(1995, 12, 14),
                    Email = "chujkosergey@ukr.net",
                    FirstName = "Sergey",
                    LastName = "Chujko",
                    Gender = true,
                    MiddleName = "Alex",
                    Phone = "+380638467591",
                    Photo = ms.ToArray()
                }));
            }
        }

        /// <summary>
        /// Method testes GetAll of PersonContext
        /// </summary>
        [TestMethod]
        public void PersonGetAll()
        {
            var result = MainContext.Instance.Person.GetAll();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Method testes Update(Set FirstName of last person = "Ivan") of PersonContext
        /// </summary>
        [TestMethod]
        public void PersonUpdate()
        {
            var item = MainContext.Instance.Person.GetAll().ToList().LastOrDefault();
            item.FirstName = "Ivan";
            Assert.IsTrue(MainContext.Instance.Person.Update(item.PersonId, item));
        }

        /// <summary>
        /// Method testes GetBy(FirstName == "Ivan") of PersonContext
        /// </summary>
        [TestMethod]
        public void PersonGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.Person.GetBy(x => x.FirstName == "Ivan"));
        }

        /// <summary>
        /// Method testes Delete(delete last inserted person) of PersonContext
        /// </summary>
        [TestMethod]
        public void LogDelete()
        {
            var dataSet = MainContext.Instance.Person.GetAll().ToList();
            var index = (from item in dataSet
                         select item.PersonId).Max();
            Assert.IsTrue(MainContext.Instance.Person.Delete(index));
        }
    }
}
