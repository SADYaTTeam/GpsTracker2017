using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using GpsTracker.Models.Models;
using GpsTracker.Service.Strategies.Base;
using GpsTracker.Service.Controllers;

namespace GpsTracker.Service.Strategies.ConcreateAppStrategies
{
    public class UnknownTypeStrategy : Strategy
    {
        #region Constructors

        public UnknownTypeStrategy(): base() { }

        public UnknownTypeStrategy(AppController controller) : base(controller) { }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}