using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.Messages
{
    public class SosMessage
    {
        public int UserId { get; set; }
        //public string Login { get; set; }
        //public string Password { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
