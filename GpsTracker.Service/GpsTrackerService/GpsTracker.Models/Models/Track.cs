// <copyright file="Track.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Models
{
    /// <summary>
    /// Class represents data model for Track table
    /// </summary>
    public class Track
    {
        public int TrackId { get; set; }

        public int MarkerId { get; set; }

        public int UserId { get; set; } 
    }
}
