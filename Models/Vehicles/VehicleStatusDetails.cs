using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truckonline_Demo_Widget.Models
{
    class VehicleStatusDetails : VehicleInfo
    {
        public GpsTrackingInfo position;
        public DriverActivityInfo driverActivityInfo;
        public DriverActivityInfo codriverActivityInfo;
    }
}
