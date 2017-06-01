using System;

namespace GpsTracker.Models.Messages
{
    public class SosMessage
    {
        public int UserId { get; set; }
        //public string Login { get; set; }
        //public string Password { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
