// <copyright file="TrackContext.cs" company="SADYaTTeam">
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
    using Mappers;
    using Interfaces;
    #endregion

    // DELETE THIS CLASS
    /// <summary>
    /// Class represents work with track EF context
    /// </summary>
    public class TrackContext : BaseContext, IDbContext<Models.Track, Track>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackContext" /> class
        /// </summary>
        public TrackContext()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackContext" /> class with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public TrackContext(GpsTrackingDBEntities context) : base(context) { }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete track with selected EventId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting track</param>
        /// <returns>Returns true if track was deleted and false if there're some errors
        /// (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.Track.FirstOrDefault(x => x.TrackId == id);
            if (temp == null)
            {
                return false;
            }
            Context.Track.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Models.Track> GetAll()
        {
            var temp = Context.Track.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Track>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null
        /// if there'are no rows</returns>
        public IEnumerable<Models.Track> GetBy(Expression<Func<Track, bool>> expression)
        {
            var temp = Context.Track.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Track>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new track to current table
        /// </summary>
        /// <param name="newItem">New track for table</param>
        /// <returns>Returns true if new track been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Models.Track newItem)
        {
            var transaction = Context.Database.BeginTransaction();
            try
            {
                Context.Database.ExecuteSqlCommand("INSERT INTO Track(MarkerId, UserId)" +
                                                    $"VALUES({newItem.MarkerId},{newItem.UserId})");
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

        //DELETE THIS METHOD
        public bool Insert(Models.User user, Models.Marker marker)
        {
            var transaction = Context.Database.BeginTransaction();
            try
            {
                Context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [User] ON");
                try
                {
                    var max = (from item in Context.Marker
                               select item.MarkerId).ToList().Max();
                    marker.MarkerId = max + 1;
                }
                catch (InvalidOperationException ex)
                {
                    Debug.WriteLine($"Marker set is empty. Exception:{ex.Message}");
                    marker.MarkerId = 1;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unknown exception with message:{ex.Message}");
                }
                marker.Timestamp = DateTime.Now;
                Context.Marker.Add(marker.Convert());
                Context.SaveChanges();
                Context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [User] OFF");
                var index = (from item in Context.Marker
                             where item.UserId == user.UserId
                             select item.MarkerId).ToList().Max();
                Context.Database.ExecuteSqlCommand("INSERT INTO Track(MarkerId, UserId)" +
                                                        $"VALUES({index},{user.UserId})");
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
        /// Update track with select id
        /// </summary>
        /// <remarks>Old track take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected track</param>
        /// <param name="newItem">Represents new info for selected track</param>
        /// <returns>Returns true if track was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Models.Track newItem)
        {
            var temp = Context.Track.FirstOrDefault(x => x.TrackId == id);
            if (temp == null)
            {
                return false;
            }
            temp.MarkerId = newItem.MarkerId;
            temp.UserId = newItem.UserId;
            return SaveChanges();
        }

        #endregion
    }
}
