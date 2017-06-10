// <copyright file="CheckMessage.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Messages
{
    /// <summary>
    /// Class represents structure of user check message
    /// </summary>
    public class CheckMessage
    {
        public string DeviceId { get; set; }
        public int? UserId { get; set; }
        public int? LogId { get; set; }
        public int? PersonId { get; set; }
        public int? EventId { get; set; }
    }
}
