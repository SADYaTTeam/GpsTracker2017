using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpsTracker.Models.Mappers;
using GpsTracker.Models.DataContext.Interfaces;
using GpsTracker.Models.Models;
using System.Linq.Expressions;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class LogContext : IDbContext<Models.Log, Log>
    {
        #region Constructors

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Log> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Log> GetBy(Expression<Func<Log, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Models.Log newItem)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Models.Log newItem)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
