namespace FleetManagementLibrary.Models.Entities
{
    public class RectangleGeofence
    {
        public long ID { get; set; }
        public long GeofenceID { get; set; }
        public float North { get; set; }
        public float East { get; set; }
        public float West { get; set; }
        public float South { get; set; }
    }
}
