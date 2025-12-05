namespace CarParkAPI.Models.DTOs
{
    public class ParkingCharge_Dto
    {
        public string VehicleReg { get; set; }
        public double VehicleCharge { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
    }
}
