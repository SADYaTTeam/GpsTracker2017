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
    public class TrackContext : BaseContext, IDbContext<Models.Track, Track>
    {
        #region Constructors

        public TrackContext(): base() { }

        public TrackContext(GpsTrackingDBEntities context): base(context) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(int id)
        {
            var temp = _context.Track.FirstOrDefault(x => x.TrackId == id);
            if (temp == null)
            {
                return false;
            }
            _context.Track.Remove(temp);
            return SaveChanges();
        }

        public IEnumerable<Models.Track> GetAll()
        {
            var temp = _context.Track.ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Track>(temp.Count);
            foreach (var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public IEnumerable<Models.Track> GetBy(Expression<Func<Track, bool>> expression)
        {
            var temp = _context.Track.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return null;
            }
            var result = new List<Models.Track>(temp.Count);
            foreach (var item in temp)
            {
                result.Add(item.Convert());
            }
            return result;
        }

        public bool Insert(Models.Track newItem)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlCommand("INSERT INTO Track(MarkerId, UserId)" +
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

        public bool Insert(Models.User user, Models.Marker marker)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [User] ON");
                try
                {
                    var max = (from item in _context.Marker
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
                _context.Marker.Add(marker.Convert());
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [User] OFF");
                _context.Database.ExecuteSqlCommand("INSERT INTO Track(MarkerId, UserId)" +
                                                    $"VALUES({marker.MarkerId},{user.UserId})");
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

        public bool Update(int id, Models.Track newItem)
        {
            var temp = _context.Track.FirstOrDefault(x => x.TrackId == id);
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
