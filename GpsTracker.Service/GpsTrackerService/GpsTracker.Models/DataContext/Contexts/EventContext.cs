using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GpsTracker.Models.DataContext.Interfaces;
using GpsTracker.Models.Models;
using GpsTracker.Models.Mappers;
using System.Diagnostics;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class EventContext : BaseContext, IDbContext<Models.Event, DataContext.Event>
    {
        #region Constructors

        public EventContext() : base() { }

        public EventContext(GpsTrackingDatabaseEntities context):base(context) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(int id)
        {
            var temp = _context.Event.FirstOrDefault(x => x.EventId == id);
            if (temp == null)
            {
                return false;
            }
            _context.Event.Remove(temp);
            return SaveChanges();
        }

        public IEnumerable<Models.Event> GetAll()
        {
            var temp = _context.Event.ToList();
            if(temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Event>(temp.Count);
            foreach(var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public IEnumerable<Models.Event> GetBy(Expression<Func<Event, bool>> expression)
        {
            var temp = _context.Event.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Event>(temp.Count);
            foreach (var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public bool Insert(Models.Event newItem)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlCommand("INSERT INTO Event(Name)" +
                                                    $"VALUES('{newItem.Name}')");
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                transaction.Rollback();
                transaction.Dispose();
                return false;
            }
            try
            {
                transaction.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return false;
            }
            return true;
        }

        public bool Update(int id, Models.Event newItem)
        {
            var temp = _context.Event.FirstOrDefault(x => x.EventId == id);
            if (temp == null)
            {
                return false;
            }
            temp.EventId = newItem.EventId;
            temp.Name = newItem.Name;
            return SaveChanges();
        }

        #endregion
    }
}
