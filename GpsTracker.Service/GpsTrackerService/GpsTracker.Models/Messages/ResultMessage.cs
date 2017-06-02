// <copyright file="ResultMessage.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Messages
{
    /// <summary>
    /// Types of result message
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Generates if there's an unexpected exception 
        /// while proccess
        /// </summary>
        UnknownError = -1,
        /// <summary>
        /// Generates after successfully ending of proccess
        /// </summary>
        Success = 0,
        /// <summary>
        /// Generates if access request must be declined
        /// </summary>
        Decline = 1
    }

    /// <summary>
    /// Class represents result of service process
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// Type of message
        /// </summary>
        public ResultType Type { get; set; }

        /// <summary>
        /// Commentary for message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Possible result longitude
        /// </summary>
        public float? Longitude { get; set; }

        /// <summary>
        /// Possible result latitude
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// Possible result radius of zone
        /// </summary>
        public float? Radius { get; set; }
    }
}