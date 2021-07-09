using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truckonline_Demo_Widget.Models
{
    class DriverInfo
    {
        public string activityUid;
        public DriverAddressInfo address;
        public DriverAddressInfo attachmentAddress;
        public string birthdate;
        public string comment;
        public string companyTimeZone;
        public string compensatoryRest;
        public string compensatoryRestDate;
        public string creationDate;
        public string customIdentifier;
        public string driverCardId;
        public bool driverEnabled;
        public string driverFirstName;
        public string driverLastName;
        public string driverUid;
        public string drivingLicenceIssuingAuthority;
        public string drivingLicenceIssuingNation;
        public string drivingLicenceNumber;
        public string email;
        public string? hiringDate;
        public string? hourlyRate;
        public string locale;
        public string operationUid;
        public float? paidLeave;
        public object? paidLeaveDate;
        public string partialName;
        public string phone;
        public string shortName;
        public float? totalCompensatoryRest;
        public float? totalPaidLeave;
        public string? userAccountUid;
        public List<string>? labels;
    }
}
