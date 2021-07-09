using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truckonline_Demo_Widget.Models
{
    class DriverStatusDetails : DriverInfo
    {
        public GpsTrackingInfo position;
        public DriverActivityInfo driverActivityInfo;
    }
}
