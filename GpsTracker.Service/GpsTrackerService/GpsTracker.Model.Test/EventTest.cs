using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GpsTracker.Models;
using System.Collections.Generic;
using System.Linq;
using GpsTracker.Models.Mappers;
using GpsTracker.Models.DataContext.Contexts;

namespace GpsTracker.Models.Test
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void EventGetAll()
        {
            Assert.IsNotNull(MainContext.Instance.Event.GetAll());
        }

        [TestMethod]
        public void EventGetBy()            
        {
            var result = MainContext.Instance.Event.GetBy(x => x.Name == "DELETE_USER");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EventInsert()
        {
            Assert.IsTrue(MainContext.Instance.Event.Insert(new Models.Event() { Name = "Test Event" }));
        }

        [TestMethod]
        public void EventUpdate()
        {
            var newEvent = MainContext.Instance.Event.GetAll().ToList().LastOrDefault().Convert();
            newEvent.Name = "Updated Test Event";
            Assert.IsTrue(MainContext.Instance.Event.Update(newEvent.EventId, newEvent.Convert()));
        }

        [TestMethod]
        public void EventDelete()
        {
            var context = new DataContext.Contexts.MainContext(new DataContext.GpsTrackingDatabaseEntities());
            var allData = context.Event.GetAll().ToList();
            Assert.IsTrue(context.Event.Delete(context.Event.GetAll().ToList().LastOrDefault().EventId));
        }
    }
}
