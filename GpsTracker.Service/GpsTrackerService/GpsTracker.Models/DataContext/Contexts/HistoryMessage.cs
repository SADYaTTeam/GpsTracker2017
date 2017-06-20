using System;

namespace GpsTracker.Models.DataContext.Contexts
{
    public class HistoryMessage
    {
        public int UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}