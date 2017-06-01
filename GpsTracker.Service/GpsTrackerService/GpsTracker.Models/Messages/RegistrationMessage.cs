// <copyright file="LoginMessage.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Messages
{
    /// <summary>
    /// Class represents structure of registration message from mobile app
    /// </summary>
    public class RegistrationMessage
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
    }
}
