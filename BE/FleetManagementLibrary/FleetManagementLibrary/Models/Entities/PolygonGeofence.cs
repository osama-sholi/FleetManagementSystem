namespace FleetManagementLibrary.Models.Entities
{
    public class PolygonGeofence
    {
        public long ID { get; set; }
        public long GeofenceID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
