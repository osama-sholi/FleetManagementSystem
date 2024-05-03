using FleetManagementLibrary.Models;
using FleetManagementLibrary.Models.Entities;
using Npgsql;
using System;
using System.Collections.Generic;

namespace FleetManagementLibrary.Data.Repositories
{
    public class GeofenceRepository
    {
        private string _connectionString;

        public GeofenceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all geofences from Geofences table
        public List<Geofences> GetAllGeofences()
        {
            // Get all geofences from Geofences table
            string query = "SELECT * FROM Geofences";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<Geofences> geofences = new List<Geofences>();

                        while (reader.Read())
                        {
                            Geofences geofence = new Geofences
                            {
                                GeofenceID = Convert.ToInt64(reader["GeofenceID"]),
                                GeofenceType = reader["GeofenceType"].ToString(),
                                AddedDate = Convert.ToInt64(reader["AddedDate"]),
                                StrockColor = reader["StrockColor"].ToString(),
                                StrockOpacity = Convert.ToSingle(reader["StrockOpacity"]),
                                StrockWeight = Convert.ToSingle(reader["StrockWeight"]),
                                FillColor = reader["FillColor"].ToString(),
                                FillOpacity = Convert.ToSingle(reader["FillOpacity"]),
                            };

                            geofences.Add(geofence);
                        }

                        return geofences;
                    }
                }
            }


        }

        // Get all circular geofences from CircleGeofence table
        public List<CircleGeofence> GetAllCircularGeofences()
        {
            // Get all circular geofences from CircleGeofence table
            string query = "Select * from CircleGeofence";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<CircleGeofence> circleGeofences = new List<CircleGeofence>();

                        while (reader.Read())
                        {
                            CircleGeofence circleGeofence = new CircleGeofence
                            {
                                ID = Convert.ToInt64(reader["ID"]),
                                GeofenceID = Convert.ToInt64(reader["GeofenceID"]),
                                Radius = Convert.ToInt64(reader["Radius"]),
                                Latitude = Convert.ToSingle(reader["Latitude"]),
                                Longitude = Convert.ToSingle(reader["Longitude"]),
                            };

                            circleGeofences.Add(circleGeofence);
                        }

                        return circleGeofences;
                    }
                }
            }


        }

        // Get all rectangular geofences from RectangleGeofence table
        public List<RectangleGeofence> GetAllRectangularGeofences()
        {

            // Get all rectangular geofences from RectangleGeofence table
            string query = "Select * from RectangleGeofence";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<RectangleGeofence> rectangleGeofences = new List<RectangleGeofence>();

                        while (reader.Read())
                        {
                            RectangleGeofence rectangleGeofence = new RectangleGeofence
                            {
                                ID = Convert.ToInt64(reader["ID"]),
                                GeofenceID = Convert.ToInt64(reader["GeofenceID"]),
                                North = Convert.ToSingle(reader["North"]),
                                East = Convert.ToSingle(reader["East"]),
                                West = Convert.ToSingle(reader["West"]),
                                South = Convert.ToSingle(reader["South"]),
                            };

                            rectangleGeofences.Add(rectangleGeofence);
                        }

                        return rectangleGeofences;
                    }
                }
            }


        }

        // Get all polygon geofences from PolygonGeofence table
        public List<PolygonGeofence> GetAllPolygonGeofences()
        {
            // Get all polygon geofences from PolygonGeofence table
            string query = "Select * from PolygonGeofence";


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<PolygonGeofence> polygonGeofences = new List<PolygonGeofence>();

                        while (reader.Read())
                        {
                            PolygonGeofence polygonGeofence = new PolygonGeofence
                            {
                                ID = Convert.ToInt64(reader["ID"]),
                                GeofenceID = Convert.ToInt64(reader["GeofenceID"]),
                                Latitude = Convert.ToSingle(reader["Latitude"]),
                                Longitude = Convert.ToSingle(reader["Longitude"]),
                            };

                            polygonGeofences.Add(polygonGeofence);
                        }

                        return polygonGeofences;
                    }
                }
            }


        }
    }
}
