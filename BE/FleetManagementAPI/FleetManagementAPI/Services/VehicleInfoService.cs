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
                throw new ResourseNotFoundException("Vehicle info not found");
            }

            // Create a new data table
            gvar.DicOfDT["VehiclesInformations"] = new DataTable();
            gvar.DicOfDT["VehiclesInformations"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("VehicleNumber", typeof(string)),
                new DataColumn("VehicleType", typeof(string)),
                new DataColumn("DriverName", typeof(string)),
                new DataColumn("PhoneNumber", typeof(string)),
                new DataColumn("LastPosition", typeof(string)),
                new DataColumn("VehicleMake", typeof(string)),
                new DataColumn("VehicleModel", typeof(string)),
                new DataColumn("LastGPSTime", typeof(string)),
                new DataColumn("LastGPSSpeed", typeof(string)),
                new DataColumn("LastAddress", typeof(string))
            });

            // Fill the data table with the data from the database
            var row = gvar.DicOfDT["VehiclesInformations"].NewRow();
            row["VehicleNumber"] = vehicleInfo.VehicleNumber;
            row["VehicleType"] = vehicleInfo.VehicleType;
            row["DriverName"] = vehicleInfo.DriverName;
            row["PhoneNumber"] = vehicleInfo.PhoneNumber;
            row["LastPosition"] = vehicleInfo.LastPosition;
            row["VehicleMake"] = vehicleInfo.VehicleMake;
            row["VehicleModel"] = vehicleInfo.VehicleModel;
            row["LastGPSTime"] = vehicleInfo.LastGPSTime;
            row["LastGPSSpeed"] = vehicleInfo.LastGPSSpeed;
            row["LastAddress"] = vehicleInfo.LastAddress;

            gvar.DicOfDT["VehiclesInformations"].Rows.Add(row);

            return gvar;
        }

        public GVAR GetAllVehiclesInfo()
        {
            GVAR gvar = new GVAR();

            var vehiclesInfo = _vehicleInfoRepository.GetAllVehiclesInfo();

            if (vehiclesInfo == null || vehiclesInfo.Count == 0)
            {
                throw new ResourseNotFoundException("No vehicles info found"); // Custom exception
            }

            // Create a new data table
            gvar.DicOfDT["VehiclesInformations"] = new DataTable();
            gvar.DicOfDT["VehiclesInformations"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("VehicleID", typeof(string)),
                new DataColumn("VehicleNumber", typeof(string)),
                new DataColumn("VehicleType", typeof(string)),
                new DataColumn("LastDirection", typeof(string)),
                new DataColumn("LastStatus", typeof(string)),
                new DataColumn("LastAddress", typeof(string)),
                new DataColumn("LastPosition", typeof(string))
            });

            // Fill the data table with the data from the database
            foreach (var vehicleInfo in vehiclesInfo)
            {
                var row = gvar.DicOfDT["VehiclesInformations"].NewRow();
                row["VehicleID"] = vehicleInfo.VehicleID;
                row["VehicleNumber"] = vehicleInfo.VehicleNumber;
                row["VehicleType"] = vehicleInfo.VehicleType;
                row["LastDirection"] = vehicleInfo.LastDirection;
                row["LastStatus"] = vehicleInfo.LastStatus;
                row["LastAddress"] = vehicleInfo.LastAddress;
                row["LastPosition"] = $"({vehicleInfo.LastLatitude},{vehicleInfo.LastLongitude})";

                gvar.DicOfDT["VehiclesInformations"].Rows.Add(row);
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

        public void UpdateVehicleInfo(GVAR gvar)
        {
            VehiclesInformations vehicleInfo = new VehiclesInformations()
            {
                VehicleID = Convert.ToInt64(gvar.DicOfDic["Tags"]["VehicleID"]),
                DriverID = Convert.ToInt64(gvar.DicOfDic["Tags"]["DriverID"]),
                VehicleMake = gvar.DicOfDic["Tags"]["VehicleMake"],
                VehicleModel = gvar.DicOfDic["Tags"]["VehicleModel"],
                PurchaseDate = Convert.ToInt64(gvar.DicOfDic["Tags"]["PurchaseDate"])
            };
            _vehicleInfoRepository.UpdateVehicleInfo(vehicleInfo);
        }

        public void DeleteVehicleInfo(long vehicleId)
        {
            _vehicleInfoRepository.DeleteVehicleInfo(vehicleId);
        }
    }
}
