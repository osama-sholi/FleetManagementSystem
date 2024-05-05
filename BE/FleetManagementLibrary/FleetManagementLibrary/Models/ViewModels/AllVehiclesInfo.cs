using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagementLibrary.Models.ViewModels
{
    public class AllVehiclesInfo
    {
        public long VehicleID { get; set; }
        public long VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public int LastDirection { get; set; }
        public char LastStatus { get; set; }
        public string LastAddress { get; set; }
        public (float, float) LastPosition { get; set; }
    }
}
