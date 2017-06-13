// <copyright file="Zone.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Models
{
    /// <summary>
    /// Class represents a data model for 'Zone' table
    /// </summary>
    public class Zone
    {
        public int ZoneId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Radius { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }
    }
}
