using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using GpsTracker.Models.Mappers;
using GpsTracker.Models.DataContext.Interfaces;
using GpsTracker.Models.Models;
using System.Linq.Expressions;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class UserContext : BaseContext, IDbContext<Models.User, DataContext.User>
    {
        #region Constructors

        public UserContext(): base() { }

        public UserContext(GpsTrackingDatabaseEntities context): base(context) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(int id)
        {
            var temp = _context.User.FirstOrDefault(x => x.UserId == id);
            if(temp == null)
            {
                return false;
            }
            _context.User.Remove(temp);
            return SaveChanges();
        }

        public IEnumerable<Models.User> GetAll()
        {
            var temp = _context.User.ToList();
            if(temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.User>(temp.Count);
            foreach(var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public IEnumerable<Models.User> GetBy(Expression<Func<User, bool>> expression)
        {
            var temp = _context.User.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.User>(temp.Count);
            foreach (var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public bool Insert(Models.User newItem)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlCommand("INSERT INTO [User](Login, Password, DeviceId, IsAdmin)" +
                                                    $"VALUES" +
                                                    $"(" +
                                                    $"'{newItem.Login}'," +
                                                    $"'{newItem.Password}'," +
                                                    $"'{newItem.DeviceId}'," +
                                                    $"{Convert.ToInt32(newItem.IsAdmin)}" +
                                                    $")");
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

        public bool Update(int id, Models.User newItem)
        {
            var temp = _context.User.FirstOrDefault(x => x.UserId == id);
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
