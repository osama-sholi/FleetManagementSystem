namespace FleetManagementLibrary.Models.Entities
{
    public class VehiclesInformations
    {
        public long ID { get; set; }
        public long VehicleID { get; set; }
        public long DriverID { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public long PurchaseDate { get; set; }
    }
}
