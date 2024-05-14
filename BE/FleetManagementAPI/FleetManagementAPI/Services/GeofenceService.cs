using FleetManagementLibrary.Data.Repositories;
using FPro;
using System.Data;

namespace FleetManagementAPI.Services
{
    public class GeofenceService
    {
        private readonly GeofenceRepository _geofenceRepository;

        public GeofenceService(String connectionString)
        {
            _geofenceRepository = new GeofenceRepository(connectionString);
        }


        public GVAR GetGeofences()
        {
            var geofences = _geofenceRepository.GetAllGeofences();

            if(geofences == null || geofences.Count == 0)
            {
                throw new ResourseNotFoundException("No geofences found"); // Custom exception
            }

            GVAR gvar = new GVAR();

            // Create a new data table
            gvar.DicOfDT["Geofences"] = new DataTable();
            gvar.DicOfDT["Geofences"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("GeofenceID"),
                new DataColumn("GeofenceType"),
                new DataColumn("AddedDate"),
                new DataColumn("StrockColor"),
                new DataColumn("StrockOpacity"),
                new DataColumn("StrockWeight"),
                new DataColumn("FillColor"),
                new DataColumn("FillOpacity")
            });

            // Fill the data table with the data from the database
            foreach (var geofence in geofences)
            {
                var row = gvar.DicOfDT["Geofences"].NewRow();
                row["GeofenceID"] = geofence.GeofenceID;
                row["GeofenceType"] = geofence.GeofenceType;
                row["AddedDate"] = geofence.AddedDate;
                row["StrockColor"] = geofence.StrockColor;
                row["StrockOpacity"] = geofence.StrockOpacity;
                row["StrockWeight"] = geofence.StrockWeight;
                row["FillColor"] = geofence.FillColor;
                row["FillOpacity"] = geofence.FillOpacity;
                gvar.DicOfDT["Geofences"].Rows.Add(row);
            }
            return gvar;
        }

        public GVAR GetCircularGeofences()
        {
            var geofences = _geofenceRepository.GetAllCircularGeofences();

            if(geofences == null || geofences.Count == 0)
            {
                throw new ResourseNotFoundException("No circular geofences found"); // Custom exception
            }

            GVAR gvar = new GVAR();

            // Create a new data table
            gvar.DicOfDT["CircularGeofences"] = new DataTable();
            gvar.DicOfDT["CircularGeofences"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("GeofenceID"),
                new DataColumn("Latitude"),
                new DataColumn("Longitude"),
                new DataColumn("Radius")
            });

            // Fill the data table with the data from the database
            foreach (var geofence in geofences)
            {
                var row = gvar.DicOfDT["CircularGeofences"].NewRow();
                row["ID"] = geofence.ID;
                row["GeofenceID"] = geofence.GeofenceID;
                row["Latitude"] = geofence.Latitude;
                row["Longitude"] = geofence.Longitude;
                row["Radius"] = geofence.Radius;

                gvar.DicOfDT["CircularGeofences"].Rows.Add(row);
            }
            return gvar;
        }

        public GVAR GetRectangularGeofences()
        {
            var geofences = _geofenceRepository.GetAllRectangularGeofences();

            if(geofences == null || geofences.Count == 0)
            {
                throw new ResourseNotFoundException("No rectangular geofences found"); // Custom exception
            }

            GVAR gvar = new GVAR();

            // Create a new data table
            gvar.DicOfDT["RectangleGeofences"] = new DataTable();
            gvar.DicOfDT["RectangleGeofences"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("GeofenceID"),
                new DataColumn("North"),
                new DataColumn("East"),
                new DataColumn("West"),
                new DataColumn("South")
            });

            // Fill the data table with the data from the database
            foreach (var geofence in geofences)
            {
                var row = gvar.DicOfDT["RectangleGeofences"].NewRow();
                row["ID"] = geofence.ID;
                row["GeofenceID"] = geofence.GeofenceID;
                row["North"] = geofence.North;
                row["East"] = geofence.East;
                row["West"] = geofence.West;
                row["South"] = geofence.South;

                gvar.DicOfDT["RectangleGeofences"].Rows.Add(row);
            }

            return gvar;
        }

        public GVAR GetPolygonGeofences()
        {
            
            var geofences = _geofenceRepository.GetAllPolygonGeofences();

            if(geofences == null || geofences.Count == 0)
            {
                throw new ResourseNotFoundException("No polygon geofences found"); // Custom exception
            }

            GVAR gvar = new GVAR();

            // Create a new data table
            gvar.DicOfDT["PolygonGeofences"] = new DataTable();
            gvar.DicOfDT["PolygonGeofences"].Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("GeofenceID"),
                new DataColumn("Latitude"),
                new DataColumn("Longitude")
            });

            // Fill the data table with the data from the database
            foreach (var geofence in geofences)
            {
                var row = gvar.DicOfDT["PolygonGeofences"].NewRow();
                row["ID"] = geofence.ID;
                row["GeofenceID"] = geofence.GeofenceID;
                row["Latitude"] = geofence.Latitude;
                row["Longitude"] = geofence.Longitude;

                gvar.DicOfDT["PolygonGeofences"].Rows.Add(row);
            }

            return gvar;
        }
    }
}
