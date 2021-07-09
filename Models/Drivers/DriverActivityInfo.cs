using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truckonline_Demo_Widget.Models
{
    class DriverActivityInfo
    {
        public string activity;
        public string startDate;
        public string endDate;
        public string creationDate;
        public string sourceType;
        public float? version;
        public bool? isModified;
        public float? latitude;
        public float? longitude;
        public float? altitude;
        public string? activityLabel;
        public string position;
        public string? manualModificationType;
        public string tag;
        public float? startKm;
        public float? endKm;
        public float? msgId;
        public string? driverUid;
        public string? vehicleUid;
        public float? preId;
        public float? curId;
        public bool? isManualEntry;
        public string uid;
        public List<string>? labels;
    }
}
