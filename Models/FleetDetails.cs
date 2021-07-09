using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truckonline_Demo_Widget.Models
{
    class FleetDetails
    {
        public CompanyInfo companyInfo;
        public List<OperationInfo> operations;
        public List<ActivityInfo> groups;
        public List<DriverStatusDetails> drivers;
        public List<VehicleStatusDetails> vehicles;
    }
}
