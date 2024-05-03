namespace FleetManagementLibrary.Models.Entities
{
    public class Geofences
    {
        public long GeofenceID { get; set; }
        public string GeofenceType { get; set; }
        public long AddedDate { get; set; }
        public string StrockColor { get; set; }
        public float StrockOpacity { get; set; }
        public float StrockWeight { get; set; }
        public string FillColor { get; set; }
        public float FillOpacity { get; set; }
    }
}
