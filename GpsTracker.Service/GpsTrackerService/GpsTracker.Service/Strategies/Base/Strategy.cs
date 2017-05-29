using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using GpsTracker.Models.Models;

namespace GpsTracker.Service.Strategies.Base
{
    public abstract class Strategy
    {
        #region Constructors
        
        public Strategy(){ }

        public Strategy(ApiController controller)
        {
            _controller = controller;
        }

        #endregion

        #region Fields

        protected ApiController _controller;

        #endregion

        #region Properties
        #endregion

        #region Methods

        public virtual IHttpActionResult Execute(GeoMessage message)
        {
            return new System.Web.Http.Results.BadRequestResult(_controller);
        }

        protected virtual void WriteToDb(GeoMessage message) { }

        #endregion
    }
}