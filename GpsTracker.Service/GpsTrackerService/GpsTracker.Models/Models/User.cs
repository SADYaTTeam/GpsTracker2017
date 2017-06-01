// <copyright file="User.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Models
{
    using System;

    /// <summary>
    /// Class represents data model for table User
    /// </summary>
    public class User
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string DeviceId { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime DateCreatedAt { get; set; }
    }
}
