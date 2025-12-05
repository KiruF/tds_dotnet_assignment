namespace CarParkAPI.Models.DTOs
{
    public class ParkedVehicle_Dto
    {
        public string VehicleReg { get; set; }
        public int SpaceNumber { get; set; }
        public DateTime TimeIn { get; set; }
    }
}
