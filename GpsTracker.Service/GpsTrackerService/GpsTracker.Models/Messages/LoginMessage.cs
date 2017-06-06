// <copyright file="LoginMessage.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Messages
{
    /// <summary>
    /// Class represents structure of login message from mobile app
    /// </summary>
    public class LoginMessage
    {
        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User deviceId
        /// </summary>
        public string DeviceId { get; set; }
    }
}
