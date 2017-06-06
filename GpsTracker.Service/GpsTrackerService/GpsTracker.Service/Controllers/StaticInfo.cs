// <copyright file="StaticInfo.cs" company="SADYaTTeam">
//     SADYaTTeam 2017.
// </copyright>
namespace GpsTracker.Service.Controllers
{
    #region using...
    using System.Collections.Generic;
    using Models.Models;
    using Models.Messages;
    #endregion

    /// <summary>
    /// Class represents static info for entire service
    /// </summary>
    public static class StaticInfo
    {
        #region Fields

        /// <summary>
        /// Representes static list of sos calles
        /// </summary>
        private static HashSet<SosMessage> _sosList;

        /// <summary>
        /// Represents static list of markers
        /// </summary>
        private static HashSet<Marker> _markerList;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="StaticInfo"/> class
        /// </summary>
        static StaticInfo()
        {
            _sosList = new HashSet<SosMessage>();
            _markerList = new HashSet<Marker>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets static list of sos calles
        /// </summary>
        public static HashSet<SosMessage> SosList => _sosList ?? (_sosList = new HashSet<SosMessage>());

        /// <summary>
        /// Gets static list of markers
        /// </summary>
        public static HashSet<Marker> MarkerList => _markerList ?? (_markerList = new HashSet<Marker>());

        #endregion

        #region Methods

        #endregion
    }
}