// <copyright file="BaseContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.DataContext.Contexts
{
    #region using...
    using System;
    using System.Data.Entity;
    using System.Diagnostics;    
    #endregion

    /// <summary>
    /// Base class for all contexts to database tables
    /// </summary>
    public abstract class BaseContext: IDisposable
    { 
        #region Fields

        /// <summary>
        /// Present EF context with DB
        /// </summary>
        private GpsTrackingDBEntities _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize new instance of <see cref="BaseContext"/> class
        /// </summary>
        protected BaseContext()
        {
            _context = new GpsTrackingDBEntities();
        }

        /// <summary>
        /// Initialize new instance of <see cref="BaseContext"/> class with selected entity
        /// </summary>
        /// <param name="entity">EF data context</param>
        protected BaseContext(GpsTrackingDBEntities entity)
        {
            _context = entity;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets EF data context
        /// </summary>
        protected GpsTrackingDBEntities Context
        {
            get => _context;
            set => _context = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Dispose created in EF context transaction 
        /// </summary>
        /// <param name="transaction">Open transaction</param>
        protected void DisposeTransaction(DbContextTransaction transaction)
        {
            try
            {
                transaction.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
            }
        }

        /// <summary>
        /// SaveChanges in EF data context
        /// </summary>
        /// <remarks>Exception details search in debug</remarks>
        /// <returns>Returns false if there're exception while saving, otherwise - true</returns>
        protected bool SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return false;
            }
            return true;
        }


        /// <summary>
        /// Dispose unmanaged DbContext
        /// </summary>
        public virtual void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}
