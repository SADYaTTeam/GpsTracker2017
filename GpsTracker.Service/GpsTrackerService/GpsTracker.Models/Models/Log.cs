using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public int EventId { get; set; }
        public string Message { get; set; }
        public DateTime EventDate { get; set; } 
        public string DeviceId { get; set; }
    }
}
