using System;

namespace GpsTracker.Models.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public int UserId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public byte[] Photo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
