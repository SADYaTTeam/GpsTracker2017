// <copyright file="PersonContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

namespace GpsTracker.Models.DataContext.Contexts
{
    #region using ...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using GpsTracker.Models.Mappers;
    using GpsTracker.Models.DataContext.Interfaces;
    #endregion

    /// <summary>
    /// Class represents work with person EF context
    /// </summary>
    public class PersonContext : BaseContext, IDbContext<Models.Person, Person>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonContext" /> class
        /// </summary>
        public PersonContext()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonContext" /> class 
        /// with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public PersonContext(GpsTrackingDBEntities context) : base(context)
        {
            
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Delete person with selected EventId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting event</param>
        /// <returns>Returns true if person was deleted and false if there're 
        /// some errors (see details in debug)</returns>
        public bool Delete(int id)
        {
            var temp = Context.Person.FirstOrDefault(x => x.PersonId == id);
            if (temp == null)
            {
                return false;
            }
            Context.Person.Remove(temp);
            return SaveChanges();
        }

        /// <summary>
        /// Get all rows from current table
        /// </summary>
        /// <returns>Returns all rows or null if there're no rows in table</returns>
        public IEnumerable<Models.Person> GetAll()
        {
            var temp = Context.Person.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Person>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Get all rows that satisfied the condition
        /// </summary>
        /// <param name="expression">Lambda-expression that represents condition</param>
        /// <returns>Returns all rows that satisfied condition or null
        /// if there'are no rows</returns>
        public IEnumerable<Models.Person> GetBy(Expression<Func<Person, bool>> expression)
        {
            var temp = Context.Person.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Person>(temp.Count);
            result.AddRange(temp.Select(item => item.Convert()));
            return result;
        }

        /// <summary>
        /// Insert new person to current table
        /// </summary>
        /// <param name="newItem">New person for table</param>
        /// <returns>Returns true if new person been inserted
        /// and false if there're some errors
        /// (see details in debug)</returns>
        public bool Insert(Models.Person newItem)
        {
            try
            {
                Context.Person.Add(newItem.Convert());
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
        /// Update person with select id
        /// </summary>
        /// <remarks>Old person take all info from new(except Id)</remarks>
        /// <param name="id">Id of selected person</param>
        /// <param name="newItem">Represents new info for selected person</param>
        /// <returns>Returns true if person was updated and false if there're 
        /// some errors(see details in debug)</returns>
        public bool Update(int id, Models.Person newItem)
        {
            var temp = Context.Person.FirstOrDefault(x => x.PersonId == id);
            if (temp == null)
            {
                return false;
            }
            temp.UserId = newItem.UserId;
            temp.FirstName = newItem.FirstName;
            temp.LastName = newItem.LastName;
            temp.MiddleName = newItem.MiddleName;
            temp.Gender = newItem.Gender;
            temp.DateOfBirth = newItem.DateOfBirth;
            temp.Photo = newItem.Photo;
            temp.Email = newItem.Email;
            temp.Phone = newItem.Phone;
            return SaveChanges();
        }

        #endregion
    }
}
