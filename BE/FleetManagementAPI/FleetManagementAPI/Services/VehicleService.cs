using FleetManagementAPI.Controllers;
using FleetManagementLibrary.Data.Repositories;
using FleetManagementLibrary.Models.Entities;
using FPro;

namespace FleetManagementAPI.Services
{
    public class VehicleService
    {
        private readonly VehicleRepository _vehicleRepository;
        public VehicleService(String connectionString)
        {
            _vehicleRepository = new VehicleRepository(connectionString);
        }

        public void AddVehicle(GVAR gvar)
        {
            Vehicles vehicle = new Vehicles()
            {
                VehicleNumber = Convert.ToInt64(gvar.DicOfDic["Tags"]["VehicleNumber"]),
                VehicleType = gvar.DicOfDic["Tags"]["VehicleType"],
            };

            _vehicleRepository.AddVehicle(vehicle);
        }

        public void UpdateVehicle(GVAR gvar)
        {
            Vehicles vehicle = new Vehicles()
            {
                VehicleID = Convert.ToInt64(gvar.DicOfDic["Tags"]["VehicleID"]),
                VehicleNumber = Convert.ToInt64(gvar.DicOfDic["Tags"]["VehicleNumber"]),
                VehicleType = gvar.DicOfDic["Tags"]["VehicleType"],
            };

            _vehicleRepository.UpdateVehicle(vehicle);
        }

        public void DeleteVehicle(long id)
        {
            _vehicleRepository.DeleteVehicle(id);
        }
    }
}
