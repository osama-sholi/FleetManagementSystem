using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetManagementLibrary.Data.Repositories;
using FleetManagementLibrary.Models.Entities;

namespace FleetManagementConsole
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=osama_fms;User Id=postgres;Password=282974;";

            DriverRepository driverRepository = new DriverRepository(connectionString);
            //GeofenceRepository geofenceRepository = new GeofenceRepository(connectionString);
            //RouteHistoryRepository routeHistoryRepository = new RouteHistoryRepository(connectionString);
            //VehicleInfoRepository vehicleInfoRepository = new VehicleInfoRepository(connectionString);
            //VehicleRepository vehicleRepository = new VehicleRepository(connectionString);

            Driver(driverRepository);
            //Geofence(geofenceRepository);
            //RouteHistory(routeHistoryRepository);
            //vehicleInfo(vehicleInfoRepository);
            //Vehicle(vehicleRepository);
            Console.ReadLine();

        }
        private static void Driver(DriverRepository driverRepository)
        {
            Console.WriteLine("Driver Repository");
            Console.WriteLine("Add Driver");
            driverRepository.AddDriver(new Driver
            {
                DriverName = "Osama",
                PhoneNumber = 0599999999,
            });

            Console.WriteLine("Update Driver");
            Console.WriteLine("Id:");

            long id = long.Parse(Console.ReadLine());

            driverRepository.UpdateDriver(new Driver
            {
                DriverID = id,
                DriverName = "Osama",
                PhoneNumber = 0588888888,
            });

            Console.WriteLine("Delete Driver");
            Console.WriteLine("Id:");
            id = long.Parse(Console.ReadLine());
            driverRepository.DeleteDriver(id);

            Console.WriteLine("Get All Drivers");
            foreach (var driver in driverRepository.GetAllDrivers())
            {
                Console.WriteLine(
                    $"Driver ID: {driver.DriverID}, Driver Name: {driver.DriverName}, Phone Number: {driver.PhoneNumber}");
            }
        }

        private static void Geofence(GeofenceRepository geofenceRepository)
        {
            Console.WriteLine("Geofence Repository");
            Console.WriteLine("Get all Geofences");
            foreach (var geofence in geofenceRepository.GetAllGeofences())
            {
                Console.WriteLine(
                    $"ID: {geofence.GeofenceID}, Type: {geofence.GeofenceType}, Added Date: {geofence.AddedDate}, Strock Color: {geofence.StrockColor}, Strock Opacity: {geofence.StrockOpacity}, Strock Weight: {geofence.StrockWeight}, Fill Color: {geofence.FillColor}, Fill Opacity: {geofence.FillOpacity}");
            }

            Console.WriteLine("Get all circular Geofences");
            foreach (var circleGeofence in geofenceRepository.GetAllCircularGeofences())
            {
                Console.WriteLine(
                    $"ID: {circleGeofence.ID}, Geofence ID: {circleGeofence.GeofenceID}, Radius: {circleGeofence.Radius}, Latitude: {circleGeofence.Latitude}, Longitude: {circleGeofence.Longitude}");
            }

            Console.WriteLine("Get all polygonal Geofences");
            foreach (var polygonalGeofence in geofenceRepository.GetAllPolygonGeofences())
            {
                Console.WriteLine(
                    $"ID: {polygonalGeofence.ID}, Geofence ID: {polygonalGeofence.GeofenceID}, Latitude: {polygonalGeofence.Latitude}, Longitude: {polygonalGeofence.Longitude}");
            }

            Console.WriteLine("Get all rectangular Geofences");
            foreach (var rectangleGeofence in geofenceRepository.GetAllRectangularGeofences())
            {
                Console.WriteLine(
                    $"ID: {rectangleGeofence.ID}, Geofence ID: {rectangleGeofence.GeofenceID}, North: {rectangleGeofence.North}, South: {rectangleGeofence.South}, East: {rectangleGeofence.East}, West: {rectangleGeofence.West}");
            }

        }

        private static void RouteHistory(RouteHistoryRepository routeHistoryRepository)
        {
            Console.WriteLine("Route History Repository");
            Console.WriteLine("Add history point");
            Console.WriteLine("Vehicle ID:");
            long id = long.Parse(Console.ReadLine());
            routeHistoryRepository.AddHistoricalPoint(new RouteHistory
            {
                VehicleID = id,
                VehicleDirection = 1,
                Status = '1',
                VehicleSpeed = "100",
                RecordTime = 1612556800,
                Address = "Riyadh",
                Latitude = 24.7136f,
                Longitude = 46.6753f
            });

            Console.WriteLine("Get all historical points for a vehicle");
            Console.WriteLine("Vehicle ID:");
            long vehicleId = long.Parse(Console.ReadLine());
            Console.WriteLine("From:");
            long from = long.Parse(Console.ReadLine());
            Console.WriteLine("To:");
            long to = long.Parse(Console.ReadLine());

            foreach (var history in routeHistoryRepository.GetVehicleRouteHistory(vehicleId, from, to))
            {
                Console.WriteLine(
                    $"Vehicle ID: {history.VehicleID}, Vehicle Number: {history.VehicleNumber}, Address: {history.Address}, Status: {history.Status}, Latitude: {history.Latitude}, Longitude: {history.Longitude}, Vehicle Direction: {history.VehicleDirection}, GPS Speed: {history.GPSSpeed}, GPS Time: {history.GPSTime}");

            }
        }

        private static void vehicleInfo(VehicleInfoRepository vehicleInfoRepository)
        {
            Console.WriteLine("Vehicle Info Repository");
            Console.WriteLine("Add Vehicle Info");
            Console.WriteLine("Vehicle ID:");
            long id = long.Parse(Console.ReadLine());
            Console.WriteLine("Driver ID:");
            long driverId = long.Parse(Console.ReadLine());
            vehicleInfoRepository.AddVehicleInfo(new VehiclesInformations
            {
                VehicleID = id,
                DriverID = driverId,
                VehicleMake = "Toyota",
                VehicleModel = "Corolla",
                PurchaseDate = 1612556800
            });

            Console.WriteLine("Update Vehicle Info");
            Console.WriteLine("Vehicle ID:");
            id = long.Parse(Console.ReadLine());
            Console.WriteLine("Driver ID:");
            driverId = long.Parse(Console.ReadLine());
            vehicleInfoRepository.UpdateVehicleInfo(new VehiclesInformations
            {
                VehicleID = id,
                DriverID = driverId,
                VehicleMake = "Toyota",
                VehicleModel = "Corolla",
                PurchaseDate = 1612556800
            });

            Console.WriteLine("Delete Vehicle Info");
            Console.WriteLine("Vehicle ID:");
            id = long.Parse(Console.ReadLine());
            vehicleInfoRepository.DeleteVehicleInfo(id);

            Console.WriteLine("Get all Vehicle Info");
            foreach (var vehicleInfo in vehicleInfoRepository.GetAllVehiclesInfo())
            {
                Console.WriteLine(
                    $"Vehicle ID: {vehicleInfo.VehicleID}, Vehicle Number: {vehicleInfo.VehicleNumber}, Vehicle Type: {vehicleInfo.VehicleType}, Last Direction: {vehicleInfo.LastDirection}, Last Status: {vehicleInfo.LastStatus}, Last Address: {vehicleInfo.LastAddress}, Last Position: {vehicleInfo.LastPosition}");
            }

            Console.WriteLine("Get Vehicle Info by ID");
            Console.WriteLine("Vehicle ID:");
            long vehicleId = long.Parse(Console.ReadLine());
            var vehicle = vehicleInfoRepository.GetVehicleInfo(vehicleId);
            Console.WriteLine(
                $"Vehicle Number: {vehicle.VehicleNumber}, Vehicle Type: {vehicle.VehicleType}, Driver Name: {vehicle.DriverName}, Phone Number: {vehicle.PhoneNumber}, Last Position: {vehicle.LastPosition}, Vehicle Make: {vehicle.VehicleMake}, Vehicle Model: {vehicle.VehicleModel}, Last GPS Time: {vehicle.LastGPSTime}, Last GPS Speed: {vehicle.LastGPSSpeed}, Last Address: {vehicle.LastAddress}");
        }

        private static void Vehicle(VehicleRepository vehicleRepository)
        {
            Console.WriteLine("Vehicle Repository");
            Console.WriteLine("Add Vehicle");
            vehicleRepository.AddVehicle(new Vehicles
            {
                VehicleNumber = 1231123,
                VehicleType = "Car",
            });

            Console.WriteLine("Update Vehicle");
            Console.WriteLine("Id:");
            long id = long.Parse(Console.ReadLine());
            vehicleRepository.UpdateVehicle(new Vehicles
            {
                VehicleID = id,
                VehicleNumber = 1231123,
                VehicleType = "Bus",
            });

            Console.WriteLine("Delete Vehicle");
            Console.WriteLine("Id:");
            id = long.Parse(Console.ReadLine());
            vehicleRepository.DeleteVehicle(id);

            Console.WriteLine("Assign Driver to vehicle");
            Console.WriteLine("Vehicle ID:");
            long vehicleId = long.Parse(Console.ReadLine());
            Console.WriteLine("Driver ID:");
            long driverId = long.Parse(Console.ReadLine());

            vehicleRepository.AddDriverToVehicle(vehicleId, driverId);
        }
    }
}
