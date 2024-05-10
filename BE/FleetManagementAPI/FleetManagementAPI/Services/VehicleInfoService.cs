using FleetManagementLibrary.Data.Repositories;
using FleetManagementLibrary.Models.Entities;
using FPro;
using System.Data;

namespace FleetManagementAPI.Services
{
    public class VehicleInfoService
    {
        private readonly VehicleInfoRepository _vehicleInfoRepository;
        public VehicleInfoService(String connectionString)
        {
            _vehicleInfoRepository = new VehicleInfoRepository(connectionString);
        }

        public GVAR GetVehicleInfo(long vehicleId)
        {
            GVAR gvar = new GVAR();

            var vehicleInfo = _vehicleInfoRepository.GetVehicleInfo(vehicleId);

            if (vehicleInfo == null)
            {
                throw new Exception("Vehicle info not found");
            }

            if (vehicleInfo == null)
            {
                throw new Exception("Vehicle not found");
            }

            gvar.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

            gvar.DicOfDic["Tags"]["VehicleType"] = vehicleInfo.VehicleType;
            gvar.DicOfDic["Tags"]["DriverName"] = vehicleInfo.DriverName;
            gvar.DicOfDic["Tags"]["PhoneNumber"] = vehicleInfo.PhoneNumber.ToString();
            gvar.DicOfDic["Tags"]["LastPosition"] = vehicleInfo.LastPosition.ToString();
            gvar.DicOfDic["Tags"]["VehicleMake"] = vehicleInfo.VehicleMake;
            gvar.DicOfDic["Tags"]["VehicleModel"] = vehicleInfo.VehicleModel;
            gvar.DicOfDic["Tags"]["LastGPSTime"] = vehicleInfo.LastGPSTime.ToString();
            gvar.DicOfDic["Tags"]["LastGPSSpeed"] = vehicleInfo.LastGPSSpeed;
            gvar.DicOfDic["Tags"]["LastAddress"] = vehicleInfo.LastAddress;

            return gvar;
        }

        public GVAR GetAllVehiclesInfo()
        {
            GVAR gvar = new GVAR();

            var vehiclesInfo = _vehicleInfoRepository.GetAllVehiclesInfo();

            if (vehiclesInfo == null || vehiclesInfo.Count == 0)
            {
                throw new Exception("No vehicles info found");
            }

            gvar.DicOfDT["VehiclesInfo"] = new DataTable();

            gvar.DicOfDT["VehiclesInfo"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("VehicleID", typeof(string)),
                new DataColumn("VehicleNumber", typeof(string)),
                new DataColumn("VehicleType", typeof(string)),
                new DataColumn("LastDirection", typeof(string)),
                new DataColumn("LastStatus", typeof(string)),
                new DataColumn("LastAddress", typeof(string)),
                new DataColumn("LastPosition", typeof(string))
            });

            foreach (var vehicleInfo in vehiclesInfo)
            {
                var row = gvar.DicOfDT["VehiclesInfo"].NewRow();
                row["VehicleID"] = vehicleInfo.VehicleID;
                row["VehicleNumber"] = vehicleInfo.VehicleNumber;
                row["VehicleType"] = vehicleInfo.VehicleType;
                row["LastDirection"] = vehicleInfo.LastDirection;
                row["LastStatus"] = vehicleInfo.LastStatus;
                row["LastAddress"] = vehicleInfo.LastAddress;
                row["LastPosition"] = $"{vehicleInfo.LastLatitude},{vehicleInfo.LastLongitude}";

                gvar.DicOfDT["VehiclesInfo"].Rows.Add(row);
            }
            return gvar;
        }

        public void AddVehicleInfo(GVAR gvar)
        {
            VehiclesInformations vehicleInfo = new VehiclesInformations()
            {
                VehicleID = Convert.ToInt64(gvar.DicOfDic["Tags"]["VehicleID"]),
                DriverID = Convert.ToInt64(gvar.DicOfDic["Tags"]["DriverID"]),
                VehicleMake = gvar.DicOfDic["Tags"]["VehicleMake"],
                VehicleModel = gvar.DicOfDic["Tags"]["VehicleModel"],
                PurchaseDate = Convert.ToInt64(gvar.DicOfDic["Tags"]["PurchaseDate"])
            };
            _vehicleInfoRepository.AddVehicleInfo(vehicleInfo);
        }
    }
}
