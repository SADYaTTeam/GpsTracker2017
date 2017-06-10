// <copyright file="FriendlistContext.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Messages
{
    /// <summary>
    /// Type of search parameter
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Search for logins
        /// </summary>
        Login = 0,
        /// <summary>
        /// Search for id
        /// </summary>
        Id = 1
    }

    /// <summary>
    /// Class represents search message from web site
    /// </summary>
    public class SearchMessage
    {
        /// <summary>
        /// Search property
        /// </summary>
        public SearchType? Property { get; set; }

        /// <summary>
        /// Searching value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Optionally comment for search
        /// </summary>
        public string Comment { get; set; }
    }
}
