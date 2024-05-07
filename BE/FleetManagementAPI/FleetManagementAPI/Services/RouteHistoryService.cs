using FleetManagementLibrary.Data.Repositories;
using FleetManagementLibrary.Models.Entities;
using FPro;

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

