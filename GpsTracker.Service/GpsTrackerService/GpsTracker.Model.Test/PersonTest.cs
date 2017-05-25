using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GpsTracker.Models.DataContext.Contexts;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GpsTracker.Model.Test
{
    [TestClass]
    public class PersonTest
    {
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

        [TestMethod]
        public void PersonGetAll()
        {
            var result = MainContext.Instance.Person.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PersonUpdate()
        {
            var item = MainContext.Instance.Person.GetAll().ToList().LastOrDefault();
            item.FirstName = "Ivan";
            Assert.IsTrue(MainContext.Instance.Person.Update(item.PersonId, item));
        }

        [TestMethod]
        public void PersonGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.Person.GetBy(x => x.FirstName == "Ivan"));
        }

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
