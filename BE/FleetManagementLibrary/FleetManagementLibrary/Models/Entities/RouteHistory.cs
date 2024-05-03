namespace FleetManagementLibrary.Models.Entities
{
    public class RouteHistory
    {
        public long RouteHistoryID { get; set; }
        public long VehicleID { get; set; }
        public int VehicleDirection { get; set; }
        public char Status { get; set; }
        public string VehicleSpeed { get; set; }
        public long RecordTime { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
