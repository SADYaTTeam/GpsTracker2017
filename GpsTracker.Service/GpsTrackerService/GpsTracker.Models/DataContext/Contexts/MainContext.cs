using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpsTracker.Models.DataContext.Interfaces;
using GpsTracker.Models.Models;
using System.Diagnostics;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class MainContext : IMainDbContext
    {
        #region Constructors

        public MainContext(GpsTrackingDatabaseEntities context)
        {
            _context = context;
            Event = new EventContext(_context);
            Log = new LogContext(_context);
            Person = new PersonContext(_context);
            User = new UserContext(_context);
            Marker = new MarkerContext(_context);
            Track = new TrackContext(_context);
        }

        #endregion

        #region Fields

        private GpsTrackingDatabaseEntities _context;
        private System.Data.Entity.DbContextTransaction _transaction;
        private static IMainDbContext _instance;

        #endregion

        #region Properties

        public static IMainDbContext Instance
        {
            get
            {
                return _instance ?? (_instance = new MainContext(new GpsTrackingDatabaseEntities()));
            }
        }

        public IDbContext<Models.Event, Event> Event { get; set; }

        public IDbContext<Models.Log, Log> Log { get; set; }

        public IDbContext<Models.Person, Person> Person { get; set; }

        public IDbContext<Models.User, User> User { get; set; }

        public IDbContext<Models.Marker, Marker> Marker { get; set; }

        public IDbContext<Models.Track, Track> Track { get; set; }


        #endregion

        #region Methods

        public void BeginTransaction()
        {
            try
            {
                _transaction = _context.Database.BeginTransaction();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction.Commit();
            }
            catch(NullReferenceException ex)
            {
                Debug.WriteLine("You didn't start transaction!");
                throw new Exception($"Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                _transaction.Dispose();
                throw new Exception($"Exception: {ex.Message}");
            }
            _transaction.Dispose();
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction.Rollback();
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("You didn't start transaction!");
                throw new Exception($"Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                _transaction.Dispose();
                throw new Exception($"Exception: {ex.Message}");
            }
            _transaction.Dispose();
        }

        #endregion
    }
}
