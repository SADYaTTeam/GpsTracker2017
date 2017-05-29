using GpsTracker.Models.Models;
//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using GpsTracker.Models.Messages;

namespace GpsTracker.Service.Controllers
{
    public static class StaticInfo
    {
        #region Constructors

        static StaticInfo()
        {
            _sosList = new HashSet<SosMessage>();
            _markerList = new HashSet<Marker>();
        }

        #endregion

        #region Fields

        private static HashSet<SosMessage> _sosList;
        private static HashSet<Marker> _markerList;

        #endregion

        #region Properties

        public static HashSet<SosMessage> SosList
        {
            get
            {
                return _sosList ?? (_sosList = new HashSet<SosMessage>());
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