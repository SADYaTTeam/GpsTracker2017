// <copyright file="LogTest.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Model.Test
{
    #region using...
    using System.Linq;
    using Models.DataContext.Contexts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    #endregion

    /// <summary>
    /// Class for test event context
    /// </summary>
    [TestClass]
    public class LogTest
    {
        /// <summary>
        /// Method testes Insert(inserts test log) method of LogContext
        /// </summary>
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

        /// <summary>
        /// Method testes GetAll method of LogContext
        /// </summary>
        [TestMethod]
        public void LogGetAll()
        {
            var result = MainContext.Instance.Log.GetAll();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Method testes Update(update last log message to "Update Test LOG" 
        /// method of LogContext
        /// </summary>
        [TestMethod]
        public void LogUpdate()
        {
            var item = MainContext.Instance.Log.GetAll().ToList().LastOrDefault();
            item.Message = "Updated Test LOG";
            item.EventId++;
            Assert.IsTrue(MainContext.Instance.Log.Update(item.LogId, item));
        }

        /// <summary>
        /// Method testes GetBy(Name == "Updated Test LOG")
        /// method of LogContext
        /// </summary>
        [TestMethod]
        public void LogGetBy()
        {
            Assert.IsNotNull(MainContext.Instance.Log.GetBy(x => x.Message == "Updated Test LOG"));
        }

        /// <summary>
        /// Method testes Delete(delete last log row) method of LogContext
        /// </summary>
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
