using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.Models
{
    public class Marker
    {
        public int MarkerId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
