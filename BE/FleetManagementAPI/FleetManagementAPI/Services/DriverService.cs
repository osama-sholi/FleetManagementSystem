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

        public GVAR GetAllDrivers()
        {
            GVAR gvar = new GVAR();
            var drivers = _driverRepository.GetAllDrivers();
            if (drivers == null || drivers.Count == 0)
            {
                throw new ResourseNotFoundException("No drivers found"); // Custom exception
            }

            // Create a new data table
            gvar.DicOfDT["Drivers"] = new System.Data.DataTable();
            gvar.DicOfDT["Drivers"].Columns.AddRange(new System.Data.DataColumn[]
            {
                new System.Data.DataColumn("DriverID", typeof(string)),
                new System.Data.DataColumn("DriverName", typeof(string)),
                new System.Data.DataColumn("PhoneNumber", typeof(string))
            });

            // Fill the data table with the data from the database
            foreach (var driver in drivers)
            {
                var row = gvar.DicOfDT["Drivers"].NewRow();
                row["DriverID"] = driver.DriverID;
                row["DriverName"] = driver.DriverName;
                row["PhoneNumber"] = driver.PhoneNumber;
                gvar.DicOfDT["Drivers"].Rows.Add(row);
            }
            return gvar;
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
