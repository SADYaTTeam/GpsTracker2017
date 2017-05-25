using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GpsTracker.Models;
using System.Collections.Generic;
using System.Linq;
using GpsTracker.Models.Mappers;

namespace GpsTracker.Models.Test
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void GetAll()
        {
            var context = new DataContext.Contexts.MainContext(new DataContext.GpsTrackingDatabaseEntities());
            var result = context.Event.GetAll().ToList();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetBy()            
        {
            var context = new DataContext.Contexts.MainContext(new DataContext.GpsTrackingDatabaseEntities());
            var allData = context.Event.GetAll().ToList();
            var result = context.Event.GetBy(x => x.EventId == 1).ToList()[0];
            Assert.IsTrue(allData.FirstOrDefault(x => x.EventId == 1).Name == result.Name);
        }

        [TestMethod]
        public void Insert()
        {
            var context = new DataContext.Contexts.MainContext(new DataContext.GpsTrackingDatabaseEntities());
            var allData = context.Event.GetAll().ToList();
            Assert.IsTrue(context.Event.Insert(new Models.Event() { Name = "Test Event" }));
        }

        [TestMethod]
        public void Update()
        {
            var context = new DataContext.Contexts.MainContext(new DataContext.GpsTrackingDatabaseEntities());
            var newEvent = context.Event.GetAll().ToList().LastOrDefault().Convert();
            newEvent.Name = "Updated Test Event";
            var result = context.Event.Update(newEvent.EventId, newEvent.Convert());
            var allData = context.Event.GetAll().ToList();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete()
        {
            var context = new DataContext.Contexts.MainContext(new DataContext.GpsTrackingDatabaseEntities());
            var allData = context.Event.GetAll().ToList();
            Assert.IsTrue(context.Event.Delete(context.Event.GetAll().ToList().LastOrDefault().EventId));
        }
    }
}
