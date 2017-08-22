// <copyright file="LogContext.cs" company="SADYaTTeam">
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
    using Mappers;
    using Interfaces;
    #endregion

    /// <summary>
    /// Class represents work with event EF context
    /// </summary>
    public class LogContext : BaseContext, IDbContext<Models.Log, Log>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogContext"/> class
        /// </summary>
        public LogContext()
        {
            
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="LogContext"/> class with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public LogContext(GpsTrackingDBEntities context) : base(context)
        {
            
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete log with selected LogId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting item</param>
        /// <returns>Returns true if item was deleted and false if there're some errors
        /// (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.Log.FirstOrDefault(x => x.LogId == id);
            if (temp == null)
            {
                return false;
            }
            Context.Log.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Models.Log> GetAll()
        {
            var temp = Context.Log.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Log>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null 
        /// if there'are no rows</returns>
        public IEnumerable<Models.Log> GetBy(Expression<Func<Log, bool>> expression)
        {
            var temp = Context.Log.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Log>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new log to current table
        /// </summary>
        /// <param name="newItem">New log for table</param>
        /// <returns>Returns true if new item been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Models.Log newItem)
        {
            try
            {
                newItem.EventDate = DateTime.Now;
                Context.Log.Add(newItem.Convert());
                if (Context.SaveChanges() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in LogContext file.\n" +
                                $"Type:{ex.GetType()}\n" +
                                $"Message:{ex.Message}\n" +
                                $"InnerText:{ex.InnerException?.Message}");
                return false;
            }
        }

        /// <summary>
        /// Update log with select id
        /// </summary>
        /// <remarks>Old log take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected log</param>
        /// <param name="newItem">represents new info for selected log</param>
        /// <returns>Returns true if log was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Models.Log newItem)
        {
            var temp = Context.Log.FirstOrDefault(x => x.LogId == id);
            if (temp == null)
            {
                return false;
            }
            temp.EventId = newItem.EventId;
            temp.Message = newItem.Message;
            return SaveChanges();
        }

        #endregion
    }
}
