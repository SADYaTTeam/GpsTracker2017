using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpsTracker.Models.DataContext.Interfaces;
using GpsTracker.Models.Mappers;
using System.Diagnostics;
using GpsTracker.Models.Models;
using System.Linq.Expressions;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class MarkerContext : BaseContext, IDbContext<Models.Marker, Marker>
    {

        #region Constructors

        public MarkerContext(): base() { }

        public MarkerContext(GpsTrackingDatabaseEntities context): base(context) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(int id)
        {
            var temp = _context.Marker.FirstOrDefault(x => x.MarkerId == id);
            if (temp == null)
            {
                return false;
            }
            _context.Marker.Remove(temp);
            return SaveChanges();
        }

        public IEnumerable<Models.Marker> GetAll()
        {
            var temp = _context.Marker.ToList();
            if(temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Marker>(temp.Count);
            foreach(var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public IEnumerable<Models.Marker> GetBy(Expression<Func<Marker, bool>> expression)
        {
            var temp = _context.Marker.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Marker>(temp.Count);
            foreach (var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public bool Insert(Models.Marker newItem)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlCommand("SET IDENTITY INSERT [User] ON");
                var max = (from item in _context.Marker
                           select item.MarkerId).ToList().Max();
                newItem.MarkerId = max + 1;
                _context.Marker.Add(newItem.Convert());
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY INSERT [User] OFF");
                transaction.Commit();
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                DisposeTransaction(transaction);
                transaction.Rollback();
                return false;
            }
            DisposeTransaction(transaction);
            return true;
        }

        public bool Update(int id, Models.Marker newItem)
        {
            var temp = _context.Marker.FirstOrDefault(x => x.MarkerId == id);
            if (temp == null)
            {
                return false;
            }
            temp.UserId = newItem.UserId;
            temp.Name = newItem.Name;
            temp.Longtitude = newItem.Longtitude;
            temp.Latitude = newItem.Latitude;
            temp.Timestamp = newItem.Timestamp;
            return SaveChanges();
        }

        #endregion
    }
}
