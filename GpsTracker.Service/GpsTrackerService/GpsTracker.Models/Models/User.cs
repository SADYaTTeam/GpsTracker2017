using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateCreatedAt { get;set; }
    }
}
