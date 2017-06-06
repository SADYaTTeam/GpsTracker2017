// <copyright file="ZoneContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

using System.Linq.Expressions;

namespace GpsTracker.Models.DataContext.Contexts
{
    #region using...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GpsTracker.Models.DataContext.Interfaces;
    using Models;
    #endregion
    public class ZoneContext : BaseContext, IDbContext<Zone, DataContext.Zone>
    {
        //#region Fields

        //#endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneContext" /> class
        /// </summary>
        public ZoneContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneContext" /> class with selected entity
        /// </summary>
        /// <param name="context">EF data context</param>
        public ZoneContext(GpsTrackingDBEntities context) : base(context)
        {

        }


        #endregion

        //#region Properties

        //#endregion

        #region Methods

        /// <summary>
        /// Delete zone with selected ZoneId from table
        /// </summary>
        /// <remarks>See details about error in debug</remarks>
        /// <param name="id">Id of deleting event</param>
        /// <returns>Returns true if event was deleted and false if there're some errors
        /// (see details in debug)</returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Zone> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Zone> GetBy(Expression<Func<DataContext.Zone, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Zone newItem)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Zone newItem)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
