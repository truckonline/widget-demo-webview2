using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truckonline_Demo_Widget.Models
{
    class GpsTrackingInfo
    {
        public string? driverUid;
        public string? vehicleUid;
        public string? uid;
        public float latitude;
        public float longitude;
        public float? altitude;
        public float? cap;
        public float speedKmph;
        public string? tacographDate;
        public string? gpsDate;
        public float? totalKm;
        public float? msgId;
        public float? causesInt;
        public List<string>? causes;
    }
}
