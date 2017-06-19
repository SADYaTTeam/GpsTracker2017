// <copyright file="FriendlistContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.DataContext.Contexts
{
    #region using...
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;    
    using Interfaces;
    using Mappers;
    using Models;
    using Comparers;
    #endregion

    public class FriendlistContext: BaseContext, IDbContext<Friendlist, DataContext.Friendlist>
    {
        #region Fields

        #endregion

        #region Constructors


        /// <summary>
        /// Initializes a new instance of the <see cref="FriendlistContext" /> class
        /// </summary>
        public FriendlistContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendlistContext" /> class with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public FriendlistContext(GpsTrackingDBEntities context) : base(context)
        {

        }


        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete item with selected EventId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting item</param>
        /// <returns>Returns true if item was deleted and false if there're some errors
        /// (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.Friendlist.FirstOrDefault(x => x.ItemId == id);
            if (temp == null)
            {
                return false;
            }
            Context.Friendlist.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Models.Friendlist> GetAll()
        {
            var temp = Context.Friendlist.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Friendlist>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null
        /// if there'are no rows</returns>
        public IEnumerable<Friendlist> GetBy(Expression<Func<DataContext.Friendlist, bool>> expression)
        {
            var temp = Context.Friendlist.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Friendlist>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new item to current table
        /// </summary>
        /// <param name="newItem">New item for table</param>
        /// <returns>Returns true if new item been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Friendlist newItem)
        {
            var transaction = Context.Database.BeginTransaction();
            try
            {
                Context.Database.ExecuteSqlCommand("INSERT INTO Friendlist(Sender, Marked)" +
                                                   $"VALUES('{newItem.Sender}','{newItem.Marked}')");
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
        /// Update item with select id
        /// </summary>
        /// <remarks>Old item take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected item</param>
        /// <param name="newItem">Represents new info for selected item</param>
        /// <returns>Returns true if item was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Friendlist newItem)
        {
            var temp = Context.Friendlist.FirstOrDefault(x => x.ItemId == id);
            if (temp == null)
            {
                return false;
            }
            temp.Sender = newItem.Sender;
            temp.Marked = newItem.Marked;
            return SaveChanges();
        }

        /// <summary>
        /// Return indexes of friends of the user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Returns enumeration of indexes of friends of the user with selected userId</returns>
        public IEnumerable<int> GetFriendOfUser(int userId)
        {
            var temp = Context.Friendlist.Where(x => x.Marked == userId);
            var temp1 = Context.Friendlist.Where(x => x.Sender == userId);
            return temp.AsEnumerable().Intersect(temp1.AsEnumerable(), new FriendlistComparer()).Select(x=>x.Sender);
        }

        public IEnumerable<int> GetRequests(int userId)
        {
            var temp = Context.Friendlist.Where(x => x.Marked == userId).Select(x=>x.Sender);
            var temp1 = GetFriendOfUser(userId);
            return temp.Except(temp1);
        }

        #endregion
    }
}
