using FleetManagementLibrary.Models.Entities;
using FleetManagementLibrary.Models.ViewModels;
using Npgsql;
using System;
using System.Collections.Generic;


namespace FleetManagementLibrary.Data.Repositories
{
    public class VehicleInfoRepository
    {
        private readonly string _connectionString;

        public VehicleInfoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new vehicle info to the VehiclesInformations table 
        public void AddVehicleInfo(VehiclesInformations vehicleInfo)
        {
            // SQL query to insert a new vehicle info into the VehiclesInformations table which has the VehicleID which refrences the vehicle table,
            // DriverID which refrences the driver table, VehicleMake, VehicleModel, PurchaseDate columns
            // Keep in mind that the foreign key VehicleID is unique in the VehiclesInformations table

            string query =
                "INSERT INTO VehiclesInformations (VehicleID,DriverID,VehicleMake,VehicleModel,PurchaseDate) " +
                "VALUES (@VehicleID,@DriverID,@VehicleMake,@VehicleModel,@PurchaseDate)";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleInfo.VehicleID);
                    command.Parameters.AddWithValue("@DriverID", vehicleInfo.DriverID);
                    command.Parameters.AddWithValue("@VehicleMake", vehicleInfo.VehicleMake);
                    command.Parameters.AddWithValue("@VehicleModel", vehicleInfo.VehicleModel);
                    command.Parameters.AddWithValue("@PurchaseDate", vehicleInfo.PurchaseDate);

                    command.ExecuteNonQuery();
                }
            }


        }

        // Update an existing vehicle info in the VehiclesInformations table
        public void UpdateVehicleInfo(VehiclesInformations vehicleInfo)
        {
            // SQL query to update the vehicle info in the VehicleInformation table which has the VehicleID which refrences the vehicle table,
            // DriverID which refrences the driver table, VehicleMake, VehicleModel, PurchaseDate columns

            string query = "UPDATE VehiclesInformations SET " +
                           "DriverID = @DriverID, " +
                           "VehicleMake = @VehicleMake, " +
                           "VehicleModel = @VehicleModel, " +
                           "PurchaseDate = @PurchaseDate " +
                           "WHERE VehicleID = @VehicleID";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleInfo.VehicleID);
                    command.Parameters.AddWithValue("@DriverID", vehicleInfo.DriverID);
                    command.Parameters.AddWithValue("@VehicleMake", vehicleInfo.VehicleMake);
                    command.Parameters.AddWithValue("@VehicleModel", vehicleInfo.VehicleModel);
                    command.Parameters.AddWithValue("@PurchaseDate", vehicleInfo.PurchaseDate);

                    command.ExecuteNonQuery();
                }
            }


        }

        // Delete a vehicle info from the VehiclesInformations table
        public void DeleteVehicleInfo(long vehicleID)
        {
            // SQL query to delete the vehicle info from the VehicleInformation based on the VehicleID

            string query = "DELETE FROM VehiclesInformations WHERE VehicleID = @VehicleID";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleID);

                    command.ExecuteNonQuery();
                }
            }


        }

        // Get some specific vehicle info for all vehicles
        public List<AllVehiclesInfo> GetAllVehiclesInfo()
        {
            // SQL query to retrieve the following data for all vehicles:
            // VehicleID, VehicleNumber and VehicleType from the Vehicles table
            // LastDirection, LastStatus, LastAddress, LastLatitude and LastLongitute from the RouteHistory table by joining the tables and retrieving the last recorded record for each vehicle

            string query = "SELECT v.VehicleID, v.VehicleNumber, v.VehicleType, rh.VehicleDirection, rh.Status, rh.Address, rh.Latitude, rh.Longitude " +
                           "FROM Vehicles v " +
                           "LEFT JOIN (" + // Subquery to get the last record for each vehicle
                           "SELECT VehicleID, MAX(RouteHistoryID) as ID, VehicleDirection, Status, Address, Latitude, Longitude " +
                           "FROM RouteHistory rh1 " +
                           "WHERE RecordTime = (" +
                           "SELECT MAX(RecordTime)" +
                           "FROM RouteHistory rh2 " +
                           "WHERE rh1.VehicleID = rh2.VehicleID) " +
                           "GROUP BY VehicleID, VehicleDirection, Status, Address, Latitude, Longitude) " + // End of subquery
                           "rh ON v.VehicleID = rh.VehicleID";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<AllVehiclesInfo> allVehiclesInfo = new List<AllVehiclesInfo>();

                        while (reader.Read())
                        {
                            allVehiclesInfo.Add(new AllVehiclesInfo
                            { // We check if the value is DBNull before converting it to the desired type since we are doing a left join
                                VehicleID = Convert.ToInt64(reader["VehicleID"]),
                                VehicleNumber = Convert.ToInt64(reader["VehicleNumber"]),
                                VehicleType = reader["VehicleType"].ToString(),
                                LastDirection = reader["VehicleDirection"] == DBNull.Value ? 0 : Convert.ToInt32(reader["VehicleDirection"]),
                                LastStatus = reader["Status"] == DBNull.Value ? ' ' : Convert.ToChar(reader["Status"]),
                                LastAddress = reader["Address"] == DBNull.Value ? "" : reader["Address"].ToString(),
                                LastLatitude = reader["Latitude"] == DBNull.Value ? 0 : Convert.ToSingle(reader["Latitude"]),
                                LastLongitude = reader["Longitude"] == DBNull.Value ? 0 : Convert.ToSingle(reader["Longitude"]),
                            });
                        }

                        return allVehiclesInfo;
                    }
                }
            }


        }

        // Get some specific vehicle info for a vehicle with the given VehicleID
        public VehicleInfo GetVehicleInfo(long vehicleID)
        {
            // SQL query to retrieve the following data for the vehicle with the given VehicleID:
            // VehicleNumber, VehicleType from the Vehicles table
            // VehicleMake, VehicleModel from the VehicleInformation table by joining the VehiclesInformation table with the Vehicles table
            // DriverName, PhoneNumber from the Drivers table by joining the Drivers table with the joined tables above
            // LastPosition(Latitude,Longitude), LastGPSTime(RecordTime), LastGPSSpeed(Speed) and LastAddress from the RouteHistory table by joining the last recorded record for the vehicle with the joined tables above

            string query = "SELECT v.VehicleNumber, v.VehicleType, vi.VehicleMake, vi.VehicleModel, d.DriverName, d.PhoneNumber, " +
                           "rh.Latitude, rh.longitude, rh.RecordTime, rh.VehicleSpeed, rh.Address " +
                           "FROM Vehicles v " +
                           "LEFT JOIN VehiclesInformations vi on v.VehicleID = vi.VehicleID " +
                           "LEFT JOIN Driver d on vi.DriverID = d.DriverID " +
                           "LEFT JOIN ( " + // Subquery to get the last record for the vehicle
                           "SELECT VehicleID, Latitude, Longitude, MAX(RecordTime) as RecordTime, VehicleSpeed, Address " +
                           "FROM RouteHistory " +
                           "GROUP BY VehicleID, Latitude, Longitude, VehicleSpeed, Address) " + // End of subquery
                           "rh on v.VehicleID = rh.VehicleID " +
                           "WHERE v.VehicleID = @VehicleID";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleID);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {// We check if the value is DBNull before converting it to the desired type since we are doing a left join
                            float latitude = reader["Latitude"] == DBNull.Value ? 0 : Convert.ToSingle(reader["Latitude"]);
                            float longitude = reader["Longitude"] == DBNull.Value ? 0 : Convert.ToSingle(reader["Longitude"]);
                            return new VehicleInfo
                            {
                                VehicleNumber = Convert.ToInt64(reader["VehicleNumber"]),
                                VehicleType = reader["VehicleType"].ToString(),
                                DriverName = reader["DriverName"] == DBNull.Value ? "" : reader["DriverName"].ToString(),
                                PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? 0 : Convert.ToInt64(reader["PhoneNumber"]),
                                LastPosition = (latitude,longitude),
                                VehicleMake = reader["VehicleMake"] == DBNull.Value ? "" : reader["VehicleMake"].ToString(),
                                VehicleModel = reader["VehicleModel"] == DBNull.Value ? "" : reader["VehicleModel"].ToString(),
                                LastGPSTime = reader["RecordTime"] == DBNull.Value ? 0 : Convert.ToInt64(reader["RecordTime"]),
                                LastGPSSpeed = reader["VehicleSpeed"] == DBNull.Value ? "" : reader["VehicleSpeed"].ToString(),
                                LastAddress = reader["Address"] == DBNull.Value ? "" : reader["Address"].ToString()
                            };
                        }

                        return null;
                    }
                }
            }

        }
    }
}
