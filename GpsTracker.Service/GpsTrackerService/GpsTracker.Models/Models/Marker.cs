using System;

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
