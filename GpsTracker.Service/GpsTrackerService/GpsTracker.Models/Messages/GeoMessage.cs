using System;

namespace GpsTracker.Models.Messages
{
    public enum MessageType
    {
        SOS,
        Marker
    }

    public class GeoMessage
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string DeviceId { get; set; }
        public Nullable<MessageType> Type { get; set; }
    }
}
