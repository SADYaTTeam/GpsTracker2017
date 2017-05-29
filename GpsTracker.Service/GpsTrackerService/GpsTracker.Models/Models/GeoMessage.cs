﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.Models
{
    public enum MessageType
    {
        SOS,
        Marker
    }

    public class GeoMessage
    {
        public float Longitude { get; set; }
        public float Latitutde { get; set; }
        public string DeviceId { get; set; }
        public MessageType Type { get; set; }
    }
}
