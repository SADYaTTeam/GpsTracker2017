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

        public static bool operator ==(User a, User b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }
            var properties = a.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(a, null) != property.GetValue(b, null))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(User a, User b)
        {
            return !(a == b);
        }
    }
}
