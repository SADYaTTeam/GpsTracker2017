// <copyright file="Person.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Models
{
    using System;

    /// <summary>
    /// Class represents data model for Person table
    /// </summary>
    public class Person
    {
        public int PersonId { get; set; }

        public int UserId { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public bool? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public static bool operator ==(Person a, Person b)
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

        public static bool operator !=(Person a, Person b)
        {
            return !(a == b);
        }
    }
}
