// <copyright file="MainContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

namespace GpsTracker.Models.DataContext.Contexts
{
    #region using...
    using System;
    using Interfaces;
    using System.Diagnostics;
    #endregion

    /// <summary>
    /// Class represents work with db EF context
    /// </summary>
    public class MainContext : IMainDbContext
    {
        #region Fields

        /// <summary>
        /// Represents singltone object of EF context
        /// </summary>
        private static IMainDbContext _instance;
        /// <summary>
        /// Represents EF context
        /// </summary>
        private readonly GpsTrackingDBEntities _context;
        /// <summary>
        /// Represents transaction in EF context
        /// </summary>
        private System.Data.Entity.DbContextTransaction _transaction;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="MainContext"/> class
        /// </summary>
        /// <param name="context">EF database context</param>
        public MainContext(GpsTrackingDBEntities context)
        {
            _context = context;
            Event = new EventContext(_context);
            Log = new LogContext(_context);
            Person = new PersonContext(_context);
            User = new UserContext(_context);
            Marker = new MarkerContext(_context);
            Friendlist = new FriendlistContext(_context);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets singletone object 
        /// </summary>
        public static IMainDbContext Instance => _instance ?? (_instance = new MainContext(new GpsTrackingDBEntities()));

        /// <summary>
        /// Gets or sets <see cref="Contexts.EventContext"/> 
        /// </summary>
        public IDbContext<Models.Event, Event> Event { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.LogContext"/> 
        /// </summary>
        public IDbContext<Models.Log, DataContext.Log> Log { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.PersonContext"/> 
        /// </summary>
        public IDbContext<Models.Person, DataContext.Person> Person { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.UserContext"/> 
        /// </summary>
        public IDbContext<Models.User, DataContext.User> User { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.MarkerContext"/> 
        /// </summary>
        public IDbContext<Models.Marker, DataContext.Marker> Marker { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Contexts.FriendlistContext"/> 
        /// </summary>
        public IDbContext<Models.Friendlist, DataContext.Friendlist> Friendlist { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Begin transaction in EF db context
        /// </summary>
        /// <remarks>See details about exception in debug</remarks>
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

        /// <summary>
        /// Commit started transaction in EF db context
        /// </summary> 
        /// <remarks>See details about exception in debug</remarks>
        public void CommitTransaction()
        {
            try
            {
                _transaction.Commit();
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

        /// <summary>
        /// Rollback started transaction in EF db context
        /// </summary>
        /// <remarks>See details about exception in debug</remarks>
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
