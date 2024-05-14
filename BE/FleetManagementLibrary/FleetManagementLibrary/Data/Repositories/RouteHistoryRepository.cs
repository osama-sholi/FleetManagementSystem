using FleetManagementLibrary.Models.Entities;
using FleetManagementLibrary.Models.ViewModels;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FleetManagementLibrary.Data.Repositories
{
    public class RouteHistoryRepository
    {
        private readonly string _connectionString;

        public RouteHistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new route history point to the RouteHistory table
        public void AddHistoricalPoint(RouteHistory routeHistory)
        {
            // SQL query to insert a new route history point into the RouteHistory table which has the RouteHistoryID (primary key),
            // VehicleID which refrences the vehicle table, VehicleDirection, Status, VehicleSpeed, RecordTime, Address, Latitude, Longitude columns

            string query =
                "INSERT INTO RouteHistory (VehicleID,VehicleDirection,Status,VehicleSpeed,RecordTime,Address,Latitude,Longitude) " +
                "VALUES (@VehicleID,@VehicleDirection,@Status,@VehicleSpeed,@RecordTime,@Address,@Latitude,@Longitude)";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", routeHistory.VehicleID);
                    command.Parameters.AddWithValue("@VehicleDirection", routeHistory.VehicleDirection);
                    command.Parameters.AddWithValue("@Status", routeHistory.Status);
                    command.Parameters.AddWithValue("@VehicleSpeed", routeHistory.VehicleSpeed);
                    command.Parameters.AddWithValue("@RecordTime", routeHistory.RecordTime);
                    command.Parameters.AddWithValue("@Address", routeHistory.Address);
                    command.Parameters.AddWithValue("@Latitude", routeHistory.Latitude);
                    command.Parameters.AddWithValue("@Longitude", routeHistory.Longitude);

                    command.ExecuteNonQuery();
                }
            }

        }

        // Get the route history of a vehicle based on the VehicleID within the given time range
        public List<VehicleRouteHistory> GetVehicleRouteHistory(long vehicleID, long startPoint, long endPoint)
        {
            // SQL query to get the following based on the VehicleID:
            // VehicleNumber from the Vehicles table
            // Address, Status, Latitude, Longitude, VehicleDirection, VehicleSpeed, RecordTime from the RouteHistory table
            //  withing the given time range by joining the Vehicles with the records that has the record time between the start and end point epoch time

            string query =
                "Select v.VehicleNumber, rh.Address, rh.Status, rh.Latitude, rh.Longitude, rh.VehicleDirection, rh.VehicleSpeed, rh.RecordTime " +
                "FROM Vehicles v INNER JOIN " +
                "(SELECT * FROM RouteHistory " +
                "WHERE VehicleID = @VehicleID AND RecordTime BETWEEN @StartPoint AND @EndPoint)" +
                " rh ON v.VehicleID = rh.VehicleID";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleID);
                    command.Parameters.AddWithValue("@StartPoint", startPoint);
                    command.Parameters.AddWithValue("@EndPoint", endPoint);

                    using (var reader = command.ExecuteReader())
                    {
                        List<VehicleRouteHistory> vehicleRouteHistories = new List<VehicleRouteHistory>();

                        while (reader.Read())
                        {
                            VehicleRouteHistory vehicleRouteHistory = new VehicleRouteHistory
                            {
                                VehicleID = vehicleID,
                                VehicleNumber = Convert.ToInt64(reader["VehicleNumber"]),
                                Address = reader["Address"].ToString(),
                                Status = reader["Status"].ToString()[0],
                                Latitude = Convert.ToSingle(reader["Latitude"]),
                                Longitude = Convert.ToSingle(reader["Longitude"]),
                                VehicleDirection = Convert.ToInt32(reader["VehicleDirection"]),
                                GPSSpeed = reader["VehicleSpeed"].ToString(),
                                GPSTime = Convert.ToInt64(reader["RecordTime"])
                            };

                            vehicleRouteHistories.Add(vehicleRouteHistory);
                        }

                        return vehicleRouteHistories;
                    }
                }
            }

        }
    }
}
