using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpsTracker.Models.Mappers;
using GpsTracker.Models.DataContext.Interfaces;
using System.Diagnostics;
using GpsTracker.Models.Models;
using System.Linq.Expressions;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class PersonContext : BaseContext, IDbContext<Models.Person, Person>
    {
        #region Constructors

        public PersonContext() : base() { }

        public PersonContext(GpsTrackingDatabaseEntities context) : base(context) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(int id)
        {
            var temp = _context.Person.FirstOrDefault(x => x.PersonId == id);
            if (temp == null)
            {
                return false;
            }
            _context.Person.Remove(temp);
            return SaveChanges();
        }

        public IEnumerable<Models.Person> GetAll()
        {
            var temp = _context.Person.ToList();
            if(temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Person>(temp.Count);
            foreach(var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public IEnumerable<Models.Person> GetBy(Expression<Func<Person, bool>> expression)
        {
            var temp = _context.Person.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Person>(temp.Count);
            foreach (var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public bool Insert(Models.Person newItem)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [User] ON");
                try
                {
                    var max = (from item in _context.Person
                               select item.PersonId).ToList().Max();
                    newItem.PersonId = max + 1;
                }
                catch (InvalidOperationException operationEx)
                {
                    Debug.WriteLine($"Person set is empty. InvalidOperationException: {operationEx.Message}");
                    newItem.PersonId = 1;
                }
                catch(Exception ex)
                {
                    throw new Exception($"Unknown exception with message: {ex.Message}");
                }
                _context.Person.Add(newItem.Convert());
                _context.SaveChanges();
                transaction.Rollback();
                DisposeTransaction(transaction);
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [User] OFF");
                transaction.Commit();
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                transaction.Rollback();
                DisposeTransaction(transaction);
                return false;
            }
            DisposeTransaction(transaction);
            return true;
        }

        public bool Update(int id, Models.Person newItem)
        {
            var temp = _context.Person.FirstOrDefault(x => x.PersonId == id);
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
