//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Http;
//using System.Web.Http.Results;
using GpsTracker.Models.Messages;
using GpsTracker.Models.Models;
using GpsTracker.Models.DataContext.Contexts;
using System.Linq;

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

        protected virtual User GetOrCreateUser(GeoMessage message)
        {
            var exist = MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId);
            if (exist == null)
            {
                MainContext.Instance.User.Insert(new User()
                {
                    DeviceId = message.DeviceId,
                    IsAdmin = false,
                    Login = message.DeviceId,
                    Password = message.DeviceId
                });
                exist = MainContext.Instance.User.GetBy(x => x.DeviceId == message.DeviceId);
            }
            return exist.ToList()[0];
        }

        #endregion
    }
}