namespace FleetManagementLibrary.Models
{
    public class CircleGeofence
    {
        public long ID { get; set; }
        public long GeofenceID { get; set; }
        public long Radius { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
