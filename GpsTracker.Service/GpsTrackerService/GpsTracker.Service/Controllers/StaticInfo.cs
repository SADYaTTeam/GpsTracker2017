using GpsTracker.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpsTracker.Service.Controllers
{
    public static class StaticInfo
    {
        #region Constructors

        static StaticInfo()
        {
            _sosList = new List<GeoMessage>();
            _markerList = new HashSet<Marker>();
        }

        #endregion

        #region Fields

        private static List<Log> _sosList;
        private static HashSet<Marker> _markerList;

        #endregion

        #region Properties

        public static List<Log> SosList
        {
            get
            {
                return _sosList ?? (_sosList = new List<GeoMessage>());
            }
        }

        public static HashSet<Marker> MarkerList
        {
            get
            {
                return _markerList ?? (_markerList = new HashSet<Marker>());
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}