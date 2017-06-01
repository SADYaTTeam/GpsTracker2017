// <copyright file="Log.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>

namespace GpsTracker.Models.Models
{
    using System;

    /// <summary>
    /// Class represents data model for Log
    /// </summary>
    public class Log
    {
        public int LogId { get; set; }

        public int EventId { get; set; }

        public string Message { get; set; }

        public DateTime EventDate { get; set; } 

        public string DeviceId { get; set; }
    }
}
