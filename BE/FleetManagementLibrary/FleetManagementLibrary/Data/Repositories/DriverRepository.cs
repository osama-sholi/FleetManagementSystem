using FleetManagementLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using Npgsql;


namespace FleetManagementLibrary.Data.Repositories
{
    public class DriverRepository
    {
        private readonly string _connectionString;

        public DriverRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new driver to the Drivers table
        public void AddDriver(Driver driver)
        {
            // SQL query to insert a new driver into the Drivers table which has the DriverID (primary key), DriverName, and PhoneNumber columns

            string query = "INSERT INTO Driver (DriverName, PhoneNumber) VALUES (@DriverName, @PhoneNumber)";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverName", driver.DriverName);
                    command.Parameters.AddWithValue("@PhoneNumber", driver.PhoneNumber);

                    command.ExecuteNonQuery();
                }
            }


        }

        // Update an existing driver in the Drivers table
        public void UpdateDriver(Driver driver)
        {
            // SQL query to update an existing driver in the Drivers table based on the DriverID

            string query =
                "UPDATE Driver SET ";

            if (!string.IsNullOrEmpty(driver.DriverName)) // To keep old value if new value is not provided
                query += "DriverName = @DriverName, ";

            if (driver.PhoneNumber != 0)
                query += "PhoneNumber = @PhoneNumber ";

            query += "WHERE DriverID = @DriverID";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driver.DriverID);

                    if (!string.IsNullOrEmpty(driver.DriverName))
                        command.Parameters.AddWithValue("@DriverName", driver.DriverName);

                    if (driver.PhoneNumber != 0)
                        command.Parameters.AddWithValue("@PhoneNumber", driver.PhoneNumber);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a driver from the Drivers table based on the DriverID
        public void DeleteDriver(long driverID)
        {
            // SQL query to delete a driver from the Drivers table based on the DriverID

            string query = "DELETE FROM Driver WHERE DriverID = @DriverID";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverID);

                    command.ExecuteNonQuery();
                }
            }

        }

        // Get all drivers from the Drivers table
        public List<Driver> GetAllDrivers()
        {
            List<Driver> drivers = new List<Driver>();

            // SQL query to select all drivers from the Drivers table

            string query = "SELECT * FROM Driver";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Driver driver = new Driver
                            {
                                DriverID = Convert.ToInt64(reader["DriverID"]),
                                DriverName = reader["DriverName"].ToString(),
                                PhoneNumber = Convert.ToInt64(reader["PhoneNumber"])
                            };

                            drivers.Add(driver);
                        }
                    }
                }
            }


            return drivers;

        }
    }
}
