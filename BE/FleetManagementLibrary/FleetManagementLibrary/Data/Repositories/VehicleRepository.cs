using FleetManagementLibrary.Models.Entities;
using Npgsql;
using System;

namespace FleetManagementLibrary.Data.Repositories
{
    public class VehicleRepository
    {
        private readonly string _connectionString;

        public VehicleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new vehicle to the Vehicles table
        public void AddVehicle(Vehicles vehicle)
        {
            // SQL query to insert a new vehicle into the Vehicles table which has the VehicleID (primary key), VehicleNumber and VehicleType columns
            string query = "INSERT INTO Vehicles (VehicleNumber,VehicleType) VALUES (@VehicleNumber,@VehicleType)";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleNumber", vehicle.VehicleNumber);
                    command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType);

                    command.ExecuteNonQuery();
                }
            }


        }

        // Update an existing vehicle in the Vehicles table
        public void UpdateVehicle(Vehicles vehicle)
        {
            // SQL query to update the VehicleNumber and VehicleType of a vehicle in the Vehicles table based on the VehicleID
            string query = "UPDATE Vehicles SET ";

            if (vehicle.VehicleNumber != 0) // To keep old value if new value is not provided
                query += "VehicleNumber = @VehicleNumber, ";

            if (!string.IsNullOrEmpty(vehicle.VehicleType))
                query += "VehicleType = @VehicleType";

            query += " WHERE VehicleID = @VehicleID";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);

                    if (vehicle.VehicleNumber != 0)
                        command.Parameters.AddWithValue("@VehicleNumber", vehicle.VehicleNumber);

                    if (!string.IsNullOrEmpty(vehicle.VehicleType))
                        command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType);

                    command.ExecuteNonQuery();
                }
            }


        }

        // Delete a vehicle from the Vehicles table 
        public void DeleteVehicle(long vehicleID)
        {
            // SQL query to delete a vehicle from the Vehicles table based on the VehicleID
            string query = "DELETE FROM Vehicles WHERE VehicleID = @VehicleID";


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

        // Assign a driver to a vehicle
        public void AddDriverToVehicle(long vehicleID, long driverID)
        {
            // SQL query to assign a driver to a vehicle by updating the DriverID column in the VehiclesInformations table based on the VehicleID
            string query = "UPDATE VehiclesInformations SET DriverID = @DriverID WHERE VehicleID = @VehicleID";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleID);
                    command.Parameters.AddWithValue("@DriverID", driverID);

                    command.ExecuteNonQuery();
                }
            }


        }
    }
}