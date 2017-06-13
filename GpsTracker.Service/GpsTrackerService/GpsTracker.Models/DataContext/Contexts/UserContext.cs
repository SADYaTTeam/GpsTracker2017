// <copyright file="UserContext.cs" company="SADYaTTeam">
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

    /// <summary>
    /// Class represents work with user EF context
    /// </summary>
    public class UserContext : BaseContext, IDbContext<Models.User, DataContext.User>
    {

        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext" /> class
        /// </summary>
        public UserContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext" /> class with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public UserContext(GpsTrackingDBEntities context) : base(context)
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete user with selected UserId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting user</param>
        /// <returns>Returns true if user was deleted and false if there're some errors
        /// (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.User.FirstOrDefault(x => x.UserId == id);
            if (temp == null)
            {
                return false;
            }
            Context.User.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Models.User> GetAll()
        {
            var temp = Context.User.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.User>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null
        /// if there'are no rows</returns>
        public IEnumerable<Models.User> GetBy(Expression<Func<User, bool>> expression)
        {
            var temp = Context.User.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.User>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new user to current table
        /// </summary>
        /// <param name="newItem">New user for table</param>
        /// <returns>Returns true if new user been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Models.User newItem)
        {
            try
            {
                newItem.DateCreatedAt = DateTime.Now;
                Context.User.Add(newItem.Convert());
                if (Context.SaveChanges() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UserContext file.\n" +
                                $"Type:{ex.GetType()}\n" +
                                $"Message:{ex.Message}\n" +
                                $"InnerText:{ex.InnerException?.Message}");
                return false;
            }
        }

        /// <summary>
        /// Update user with select id
        /// </summary>
        /// <remarks>Old user take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected user</param>
        /// <param name="newItem">Represents new info for selected user</param>
        /// <returns>Returns true if user was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Models.User newItem)
        {
            var temp = Context.User.FirstOrDefault(x => x.UserId == id);
            if (temp == null)
            {
                return false;
            }
            temp.Login = newItem.Login;
            temp.Password = newItem.Password;
            temp.DeviceId = newItem.DeviceId;
            temp.IsAdmin = newItem.IsAdmin;
            temp.DateCreatedAt = newItem.DateCreatedAt;
            return SaveChanges();
        }

        #endregion
    }
}
