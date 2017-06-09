// <copyright file="EventContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.DataContext.Contexts
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Diagnostics;
    using Interfaces;
    using Mappers;
    using Models;
    #endregion

    /// <summary>
    /// Class represents work with event EF context
    /// </summary>
    public class EventContext : BaseContext, IDbContext<Event, DataContext.Event>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventContext" /> class
        /// </summary>
        public EventContext()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventContext" /> class with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public EventContext(GpsTrackingDBEntities context) : base(context)
        {
            
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete event with selected EventId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting event</param>
        /// <returns>Returns true if event was deleted and false if there're some errors
        /// (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.Event.FirstOrDefault(x => x.EventId == id);
            if (temp == null)
            {
                return false;
            }
            Context.Event.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Event> GetAll()
        {
            var temp = Context.Event.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Event>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null
        /// if there'are no rows</returns>
        public IEnumerable<Event> GetBy(Expression<Func<DataContext.Event, bool>> expression)
        {
            var temp = Context.Event.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Event>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new event to current table
        /// </summary>
        /// <param name="newItem">New event for table</param>
        /// <returns>Returns true if new event been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Event newItem)
        {
            try
            {
                Context.Event.Add(newItem.Convert());
                if (Context.SaveChanges() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in EventContext file.\n" +
                                $"Type:{ex.GetType()}\n" +
                                $"Message:{ex.Message}\n" +
                                $"InnerText:{ex.InnerException?.Message}");
                return false;
            }
        }

        /// <summary>
        /// Update event with select id
        /// </summary>
        /// <remarks>Old event take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected event</param>
        /// <param name="newItem">Represents new info for selected event</param>
        /// <returns>Returns true if event was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Event newItem)
        {
            var temp = Context.Event.FirstOrDefault(x => x.EventId == id);
            if (temp == null)
            {
                return false;
            }
            temp.EventId = newItem.EventId;
            temp.Name = newItem.Name;
            return SaveChanges();
        }

        #endregion
    }
}
