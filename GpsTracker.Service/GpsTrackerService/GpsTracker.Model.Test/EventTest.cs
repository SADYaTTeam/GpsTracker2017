// <copyright file="EventTest.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Model.Test
{
    #region using...
    using System.Linq;
    using Models.DataContext.Contexts;
    using Models.Mappers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    #endregion

    /// <summary>
    /// Class for test event context
    /// </summary>
    [TestClass]
    public class EventTest
    {
        /// <summary>
        /// Method testes GetAll method of EventContext
        /// </summary>
        [TestMethod]
        public void EventGetAll()
        {
            Assert.IsNotNull(MainContext.Instance.Event.GetAll());
        }

        /// <summary>
        /// Method testes GetBy(Name == "DELETE_USER") method of EventContext
        /// </summary>
        [TestMethod]
        public void EventGetBy()            
        {
            var result = MainContext.Instance.Event.GetBy(x => x.Name == "DELETE_USER");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Method testes Insert(inserts "Test Event") method of EventContext
        /// </summary>
        [TestMethod]
        public void EventInsert()
        {
            Assert.IsTrue(MainContext.Instance.Event.Insert(new Models.Models.Event() { Name = "Test Event" }));
        }

        /// <summary>
        /// Method testes Update("Test Event" to "Updated Test Event") method of EventContext
        /// </summary>
        [TestMethod]
        public void EventUpdate()
        {
            var newEvent = MainContext.Instance.Event.GetAll().ToList().LastOrDefault().Convert();
            newEvent.Name = "Updated Test Event";
            Assert.IsTrue(MainContext.Instance.Event.Update(newEvent.EventId, newEvent.Convert()));
        }

        /// <summary>
        /// Method testes Delete(delete last inserted event) method of EventContext
        /// </summary>
        [TestMethod]
        public void EventDelete()
        {
            var context = new MainContext(new Models.DataContext.GpsTrackingDBEntities());
            Assert.IsTrue(context.Event.Delete(context.Event.GetAll().ToList().LastOrDefault().EventId));
        }
    }
}
