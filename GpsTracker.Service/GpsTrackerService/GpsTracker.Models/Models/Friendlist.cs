// <copyright file="Friendlist.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Models.Models
{
    /// <summary>
    /// Class represents data model for 'Friendlist' table
    /// </summary>
    public class Friendlist
    {
        public int ItemId { get; set; }

        public int Sender { get; set; }

        public int Marked { get; set; }
    }
}
