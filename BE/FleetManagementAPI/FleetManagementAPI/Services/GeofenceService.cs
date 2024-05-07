using FleetManagementLibrary.Data.Repositories;
using FPro;

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
            GVAR gvar = new GVAR();

            var geofences = _geofenceRepository.GetAllGeofences();

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
            }

            return gvar;
        }

        public GVAR GetCircularGeofences()
        {
            GVAR gvar = new GVAR();

            var geofences = _geofenceRepository.GetAllCircularGeofences();

            foreach (var geofence in geofences)
            {
                var row = gvar.DicOfDT["CircleGeofences"].NewRow();
                row["ID"] = geofence.ID;
                row["GeofenceID"] = geofence.GeofenceID;
                row["Radius"] = geofence.Radius;
                row["Latitude"] = geofence.Latitude;
                row["Longitude"] = geofence.Longitude;
            }

            return gvar;
        }

        public GVAR GetRectangularGeofences()
        {
            GVAR gvar = new GVAR();

            var geofences = _geofenceRepository.GetAllRectangularGeofences();

            foreach (var geofence in geofences)
            {
                var row = gvar.DicOfDT["RectangleGeofences"].NewRow();
                row["ID"] = geofence.ID;
                row["GeofenceID"] = geofence.GeofenceID;
                row["North"] = geofence.North;
                row["East"] = geofence.East;
                row["West"] = geofence.West;
                row["South"] = geofence.South;
            }

            return gvar;
        }

        public GVAR GetPolygonGeofences()
        {
            GVAR gvar = new GVAR();

            var geofences = _geofenceRepository.GetAllPolygonGeofences();

            foreach (var geofence in geofences)
            {
                var row = gvar.DicOfDT["PolygonGeofences"].NewRow();
                row["ID"] = geofence.ID;
                row["GeofenceID"] = geofence.GeofenceID;
                row["Latitude"] = geofence.Latitude;
                row["Longitude"] = geofence.Longitude;
            }

            return gvar;
        }
    }
}
