// <copyright file="SosMessage.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Messages
{
    using System;

    /// <summary>
    /// Class represent structure for SosMessages from mobile app
    /// </summary>
    public class SosMessage
    {
        public int UserId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
