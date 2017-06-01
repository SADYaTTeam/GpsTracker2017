// <copyright file="MarkerContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.DataContext.Contexts
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using GpsTracker.Models.DataContext.Interfaces;
    using GpsTracker.Models.Mappers;
    #endregion

    /// <summary>
    /// Class represents work with marker EF context
    /// </summary>
    public class MarkerContext : BaseContext, IDbContext<Models.Marker, Marker>
    {

        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerContext"/> class
        /// </summary>
        public MarkerContext()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerContext"/> class
        /// </summary>
        /// <param name="context">EF data context</param>
        public MarkerContext(GpsTrackingDBEntities context) : base(context)
        {
            
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete marker with selected MarkerId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting marker</param>
        /// <returns>Returns true if marker was deleted and false if 
        /// there're some errors (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.Marker.FirstOrDefault(x => x.MarkerId == id);
            if (temp == null)
            {
                return false;
            }
            Context.Marker.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Models.Marker> GetAll()
        {
            var temp = Context.Marker.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Marker>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null
        /// if there'are no rows</returns>
        public IEnumerable<Models.Marker> GetBy(Expression<Func<Marker, bool>> expression)
        {
            var temp = Context.Marker.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Marker>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new marker to current table
        /// </summary>
        /// <param name="newItem">New marker for table</param>
        /// <returns>Returns true if new marker been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Models.Marker newItem)
        {
            var transaction = Context.Database.BeginTransaction();
            try
            {
                Context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Marker ON");
                try
                {
                    var max = (from item in Context.Marker
                               select item.MarkerId).ToList().Max();
                    newItem.MarkerId = max + 1;
                }
                catch (InvalidOperationException ex)
                {
                    Debug.WriteLine($"Marker set is empty. Exception:{ex.Message}");
                    newItem.MarkerId = 1;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unknown exception with message:{ex.Message}");
                }
                newItem.Timestamp = DateTime.Now;
                Context.Marker.Add(newItem.Convert());
                Context.SaveChanges();
                Context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Marker OFF");
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                transaction.Rollback();
                DisposeTransaction(transaction);
                return false;
            }
            DisposeTransaction(transaction);
            return true;
        }

        /// <summary>
        /// Update marker with select id
        /// </summary>
        /// <remarks>Old marker take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected marker</param>
        /// <param name="newItem">represents new info for selected marker</param>
        /// <returns>Returns true if marker was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Models.Marker newItem)
        {
            var temp = Context.Marker.FirstOrDefault(x => x.MarkerId == id);
            if (temp == null)
            {
                return false;
            }
            temp.UserId = newItem.UserId;
            temp.Name = newItem.Name;
            temp.Longtitude = newItem.Longtitude;
            temp.Latitude = newItem.Latitude;
            temp.Timestamp = newItem.Timestamp;
            return SaveChanges();
        }

        #endregion
    }
}
