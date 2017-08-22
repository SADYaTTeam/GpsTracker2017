// <copyright file="Marker.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Models
{
    using System;

    /// <summary>
    /// Class represents data model for Marker table
    /// </summary>
    public class Marker
    {
        public int MarkerId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
