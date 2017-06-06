// <copyright file="GeoMessage.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

namespace GpsTracker.Models.Messages
{
    /// <summary>
    /// Types of incoming marker messages
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Sos-type messages will be written to log
        /// </summary>
        Sos,
        /// <summary>
        /// Marker-type messages will be written to marker history
        /// </summary>
        Marker
    }

    /// <summary>
    /// Class represents structure of marker message from mobile app
    /// </summary>
    public class GeoMessage
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string DeviceId { get; set; }

        public MessageType? Type { get; set; }
    }
}
