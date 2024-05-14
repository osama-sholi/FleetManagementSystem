using FleetManagementLibrary.Data.Repositories;
using FleetManagementLibrary.Models.Entities;
using FPro;
using System.Data;

namespace FleetManagementAPI.Services
{
    public class RouteHistoryService
    {
        private readonly RouteHistoryRepository _routeHistoryRepository;
        public RouteHistoryService(String connectionString)
        {
            _routeHistoryRepository = new RouteHistoryRepository(connectionString);
        }

        public GVAR GetVehicleRouteHistory(long vehicleId, long startDate, long endDate)
        {
            GVAR gvar = new GVAR();

            var routes = _routeHistoryRepository.GetVehicleRouteHistory(vehicleId, startDate, endDate);

            if (routes == null || routes.Count == 0)
            {
                throw new ResourseNotFoundException("No routes found"); // Custom exception
            }

            // Create a new data table
            gvar.DicOfDT["RouteHistory"] = new DataTable();
            gvar.DicOfDT["RouteHistory"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("VehicleID", typeof(long)),
                new DataColumn("VehicleNumber", typeof(string)),
                new DataColumn("Address", typeof(string)),
                new DataColumn("Status", typeof(char)),
                new DataColumn("Latitude", typeof(float)),
                new DataColumn("Longitude", typeof(float)),
                new DataColumn("VehicleDirection", typeof(int)),
                new DataColumn("GPSSpeed", typeof(string)),
                new DataColumn("GPSTime", typeof(long))
            });

            // Fill the data table with the data from the database
            foreach (var route in routes)
            {
                var row = gvar.DicOfDT["RouteHistory"].NewRow();
                row["VehicleID"] = route.VehicleID;
                row["VehicleNumber"] = route.VehicleNumber;
                row["Address"] = route.Address;
                row["Status"] = route.Status;
                row["Latitude"] = route.Latitude;
                row["Longitude"] = route.Longitude;
                row["VehicleDirection"] = route.VehicleDirection;
                row["GPSSpeed"] = route.GPSSpeed;
                row["GPSTime"] = route.GPSTime;

                gvar.DicOfDT["RouteHistory"].Rows.Add(row);
            }

            return gvar;
        }

        public void AddRouteHistory(GVAR gvar)
        {

            RouteHistory route = new RouteHistory()
            {
                VehicleID = Convert.ToInt64(gvar.DicOfDic["Tags"]["VehicleID"]),
                VehicleDirection = Convert.ToInt32(gvar.DicOfDic["Tags"]["VehicleDirection"]),
                Status = Convert.ToChar(gvar.DicOfDic["Tags"]["Status"]),
                VehicleSpeed = gvar.DicOfDic["Tags"]["VehicleSpeed"],
                RecordTime = Convert.ToInt64(gvar.DicOfDic["Tags"]["RecordTime"]),
                Address = gvar.DicOfDic["Tags"]["Address"],
                Latitude = Convert.ToSingle(gvar.DicOfDic["Tags"]["Latitude"]),
                Longitude = Convert.ToSingle(gvar.DicOfDic["Tags"]["Longitude"])
            };

            _routeHistoryRepository.AddHistoricalPoint(route);

        }

    }
}

