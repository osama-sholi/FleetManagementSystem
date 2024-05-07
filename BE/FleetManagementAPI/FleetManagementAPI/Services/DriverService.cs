using FPro;
using FleetManagementLibrary.Models.Entities;
using FleetManagementLibrary.Data.Repositories;


namespace FleetManagementAPI.Services
{
    
    public class DriverService
    {
        private readonly DriverRepository _driverRepository;
        public DriverService(String connectionString)
        {
            _driverRepository = new DriverRepository(connectionString);
        }

        public void AddDriver(GVAR gvar)
        {
            Driver driver = new Driver()
            {
                DriverName = gvar.DicOfDic["Tags"]["DriverName"],
                PhoneNumber = Convert.ToInt64(gvar.DicOfDic["Tags"]["PhoneNumber"])
            };

            _driverRepository.AddDriver(driver);
        }

        public void UpdateDriver(GVAR gvar)
        {
            Driver driver = new Driver()
            {
                DriverID = Convert.ToInt64(gvar.DicOfDic["Tags"]["DriverID"]),
                DriverName = gvar.DicOfDic["Tags"]["DriverName"],
                PhoneNumber = Convert.ToInt64(gvar.DicOfDic["Tags"]["PhoneNumber"])
            };
            _driverRepository.UpdateDriver(driver);
        }

        public void DeleteDriver(long id)
        {
            _driverRepository.DeleteDriver(id);
        }


    }
}
