using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using GpsTracker.Models.DataContext.Contexts;
using GpsTracker.Models.Messages;
using GpsTracker.Service.Strategies.Base;
using GpsTracker.Service.Strategies.ConcreateAppStrategies;
//using System.Net;
//using System.Net.Http;

namespace GpsTracker.Service.Controllers.App
{
    [RoutePrefix ("api/app")]
    public class AppController : ApiController
    {
        public delegate void EventContainer(object sender, EventArgs args);

        //public event EventContainer onSos;


        [HttpPost]
        [Route("")]
        public IHttpActionResult TakeGeo([FromBody] GeoMessage message)
        {
            Strategy strategy;
            switch (message.Type)
            {
                case MessageType.Marker:
                    {
                        strategy = new MarkerStrategy(this);
                        break;
                    }
                case MessageType.SOS:
                    {
                        strategy = new SosStrategy(this);
                        break;
                    }
                default:
                    {
                        strategy = new UnknownTypeStrategy(this);
                        break;
                    }
            }
            return strategy.Execute(message);
        }

        [HttpPost]
        [Route("check")]
        public IHttpActionResult CheckExist([FromBody] CheckMessage message)
        {
            try
            {
                if (message.DeviceId.Length != 16)
                {
                    throw new Exception("DeviceId length != 16");
                }
                if (MainContext.Instance.User.GetBy(x => message.DeviceId == x.DeviceId) != null)
                {
                    return new System.Web.Http.Results.OkResult(this);
                }
                return new System.Web.Http.Results.BadRequestResult(this);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return new System.Web.Http.Results.InternalServerErrorResult(this);
            }
        }

        [HttpPost]
        [Route("reg")]
        public IHttpActionResult Registration([FromBody] LoginMessage newUser)
        {
            try
            {
                if(newUser.GetType().GetProperties().Where(pi => pi.GetValue(newUser) is string)
                                                    .Select(pi => (string) pi.GetValue(newUser))
                                                    .Any(value => String.IsNullOrEmpty(value)))
                {
                    return new System.Web.Http.Results.BadRequestResult(this);
                }
                if (MainContext.Instance.User.GetBy(x => x.DeviceId == newUser.DeviceId) != null)
                {
                    return new System.Web.Http.Results.BadRequestResult(this);
                }
                if(MainContext.Instance.User.Insert(new Models.Models.User() {
                                                                                Login = newUser.Login,
                                                                                Password = newUser.Password,
                                                                                DeviceId = newUser.DeviceId,
                                                                             }))
                {
                    return new System.Web.Http.Results.OkResult(this);
                }
                return new System.Web.Http.Results.BadRequestResult(this);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Internal server exception: {ex.Message}");
                return new System.Web.Http.Results.InternalServerErrorResult(this);
            }
        }
    }
}
