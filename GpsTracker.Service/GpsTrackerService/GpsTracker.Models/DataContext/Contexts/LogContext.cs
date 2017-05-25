using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpsTracker.Models.Mappers;
using GpsTracker.Models.DataContext.Interfaces;
using GpsTracker.Models.Models;
using System.Linq.Expressions;
using System.Diagnostics;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class LogContext : BaseContext, IDbContext<Models.Log, Log>
    {
        #region Constructors

        public LogContext() : base() { }

        public LogContext(GpsTrackingDatabaseEntities context) : base(context) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(int id)
        {
            var temp = _context.Log.FirstOrDefault(x => x.LogId == id);
            if (temp == null)
            {
                return false;
            }
            _context.Log.Remove(temp);
            return SaveChanges();
        }

        public IEnumerable<Models.Log> GetAll()
        {
            var temp = _context.Log.ToList();
            if(temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Log>(temp.Count);
            foreach(var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public IEnumerable<Models.Log> GetBy(Expression<Func<Log, bool>> expression)
        {
            var temp = _context.Log.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Log>(temp.Count);
            foreach (var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public bool Insert(Models.Log newItem)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlCommand("SET IDENTITY INSERT [User] ON");
                try
                {
                    var max = (from item in _context.Log
                               select item.LogId).ToList().Max();
                    newItem.LogId = max + 1;
                }
                catch (InvalidOperationException operationEx)
                {
                    Debug.WriteLine($"Person set is empty. InvalidOperationException: {operationEx.Message}");
                    newItem.LogId = 1;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unknown exception with message: {ex.Message}");
                }
                _context.Log.Add(newItem.Convert());
                _context.SaveChanges();
                transaction.Rollback();
                DisposeTransaction(transaction);
                _context.Database.ExecuteSqlCommand("SET IDENTITY INSERT [User] OFF");
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

        public bool Update(int id, Models.Log newItem)
        {
            var temp = _context.Log.FirstOrDefault(x => x.LogId == id);
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
