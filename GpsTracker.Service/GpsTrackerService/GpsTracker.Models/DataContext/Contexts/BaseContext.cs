using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
