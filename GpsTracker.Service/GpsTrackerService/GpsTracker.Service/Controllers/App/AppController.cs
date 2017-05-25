using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GpsTracker.Models.DataContext.Contexts;
using GpsTracker.Models.Models;
using System.Threading.Tasks;
using GpsTracker.Service.Strategies.Base;
using GpsTracker.Service.Strategies.ConcreateAppStrategies;

namespace GpsTracker.Service.Controllers
{
    [RoutePrefix ("api/app")]
    public class AppController : ApiController
    {
        public delegate void EventContainer(object sender, EventArgs args);

        public event EventContainer onSos;


        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> TakeGeo([FromBody] GeoMessage message)
        {
            Strategy strategy;
            switch(message.Type)
            {
                case MessageType.Marker:
                    {
                        strategy = new MarkerStrategy(this);
                        return strategy.Execute(message);
                    }
                case MessageType.SOS:
                    {
                        strategy = new SosStrategy(this);
                        var response = strategy.Execute(message);
                        onSos(null, null);
                        return response;
                    }
                default:
                    {
                        strategy = new UnknownTypeStrategy(this);
                        return strategy.Execute(message);
                    }
            }
        }
    }
}
