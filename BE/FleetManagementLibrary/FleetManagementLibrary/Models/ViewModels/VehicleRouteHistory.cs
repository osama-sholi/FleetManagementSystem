using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagementLibrary.Models.ViewModels
{
    public class VehicleRouteHistory
    {
        public long VehicleID { get; set; }
        public long VehicleNumber { get; set; }
        public string Address { get; set; }
        public char Status { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int VehicleDirection { get; set; }
        public string GPSSpeed { get; set; }
        public long GPSTime { get; set; }
    }
}
