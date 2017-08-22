// <copyright file="IDbContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.DataContext.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents interface for DbContexts with common CRUD-operations with DB
    /// </summary>
    /// <typeparam name="T">Model class</typeparam>
    /// <typeparam name="U">Equally DataContext class</typeparam>
    /// <remarks>If you set not equal DataContext class , there're will be 
    /// mapping errors</remarks>
    public interface IDbContext<T, U>
    {
        /// <summary>
        /// Delete item with selected itemId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting item</param>
        /// <returns>Returns true if item was deleted and false if there're some errors
        /// (see details in debug)</returns>
        bool Delete(int id);

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null 
        /// if there'are no rows</returns>
        IEnumerable<T> GetBy(Expression<Func<U, bool>> expression);

        /// <summary>
        /// Insert new item to current table
        /// </summary>
        /// <param name="newItem">New item for table</param>
        /// <returns>Returns true if new item been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        bool Insert(T newItem);

        /// <summary>
        /// Update item with select id
        /// </summary>
        /// <remarks>Old item take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected item</param>
        /// <param name="newItem">represents new info for selected item</param>
        /// <returns>Returns true if item was updated and false if there're 
        /// some errors(see details in debug)</returns>
        bool Update(int id, T newItem);
    }
}
