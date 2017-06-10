// <copyright file="ZoneContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

using System.Diagnostics;
using System.Linq.Expressions;
using GpsTracker.Models.Mappers;

namespace GpsTracker.Models.DataContext.Contexts
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GpsTracker.Models.DataContext.Interfaces;
    using Models;
    #endregion
    public class ZoneContext : BaseContext, IDbContext<Zone, DataContext.Zone>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneContext" /> class
        /// </summary>
        public ZoneContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneContext" /> class with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public ZoneContext(GpsTrackingDBEntities context) : base(context)
        {

        }


        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete zone with selected ZoneId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting event</param>
        /// <returns>Returns true if zone was deleted and false if there're some errors
        /// (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.Zone.FirstOrDefault(x => x.ZoneId == id);
            if (temp == null)
            {
                return false;
            }
            Context.Zone.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Zone> GetAll()
        {
            var temp = Context.Zone.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Zone>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null
        /// if there'are no rows</returns>
        public IEnumerable<Zone> GetBy(Expression<Func<DataContext.Zone, bool>> expression)
        {
            var temp = Context.Zone.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Zone>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new zone to current table
        /// </summary>
        /// <param name="newItem">New person for table</param>
        /// <returns>Returns true if new person been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Zone newItem)
        {
            try
            {
                Context.Zone.Add(newItem.Convert());
                if (Context.SaveChanges() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in PersonContext file.\n" +
                                $"Type:{ex.GetType()}\n" +
                                $"Message:{ex.Message}\n" +
                                $"InnerText:{ex.InnerException?.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Update zone with select id
        /// </summary>
        /// <remarks>Old zone take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected zone</param>
        /// <param name="newItem">Represents new info for selected zone</param>
        /// <returns>Returns true if zone was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Zone newItem)
        {
            var temp = Context.Zone.FirstOrDefault(x => x.ZoneId == id);
            if (temp == null)
            {
                return false;
            }
            temp.UserId = newItem.UserId;
            temp.Latitude = newItem.Latitude;
            temp.Longitude = newItem.Longitude;
            temp.Name = newItem.Name;
            temp.Radius = newItem.Radius;
            return SaveChanges();
        }

        #endregion
    }
}
