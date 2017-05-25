using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;

namespace GpsTracker.Models.DataContext.Contexts
{
    public abstract class BaseContext
    {
        #region Constructor

        public BaseContext()
        {
            _context = new GpsTrackingDatabaseEntities();
        }

        public BaseContext(GpsTrackingDatabaseEntities context)
        {
            _context = context;
        }

        #endregion

        #region Fields

        protected GpsTrackingDatabaseEntities _context;

        #endregion

        #region Properties
        #endregion

        #region Methods

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

        protected bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception:{ex.Message}");
                return false;
            }
            return true;
        }

        #endregion
    }
}
