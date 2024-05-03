using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagementLibrary.Models.ViewModels
{
    public class VehicleInfo
    {
        public long VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public string DriverName { get; set; }
        public long PhoneNumber { get; set; }
        public (float, float) LastPosition { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public long LastGPSTime { get; set; }
        public string LastGPSSpeed { get; set; }
        public string LastAddress { get; set; }
    }
}
