﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GpsTracker.Models.DataContext.Contexts;
using System.Linq;
using GpsTracker.Models.Mappers;

namespace GpsTracker.Model.Test
{
    [TestClass]
    public class LogTest
    {

        [TestMethod]
        public void LogInsert()
        {
            var index = MainContext.Instance.Event.GetAll().ToList()[0].EventId;
            Assert.IsTrue(MainContext.Instance.Log.Insert(new Models.Models.Log()
            {
                EventId = index,
                Message = "Test LOG"
            }));
        }

        [TestMethod]
        public void LogGetAll()
        {
            var result = MainContext.Instance.Log.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LogUpdate()
        {
            var item = MainContext.Instance.Log.GetAll().ToList().LastOrDefault();
            item.Message = "Updated Test LOG";
            item.EventId ++;
            Assert.IsTrue(MainContext.Instance.Log.Update(item.LogId, item));
        }

        [TestMethod]
        public void LogGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.Log.GetBy(x => x.Message == "Updated Test LOG"));
        }

        [TestMethod]
        public void LogDelete()
        {
            var dataSet = MainContext.Instance.Log.GetAll().ToList();
            var index = (from item in dataSet
                         select item.LogId).Max();
            Assert.IsTrue(MainContext.Instance.Log.Delete(index));
        }
    }
}
