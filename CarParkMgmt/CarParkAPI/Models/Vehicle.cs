namespace CarParkAPI.Models
{
    public class Vehicle
    {
        public int ID { get; set; }

        public VehicleType VehicleType { get; set; }
        public string Reg { get; set; }
        public DateTime TimeIn { get; set; }

        public int ParkedInID { get; set; }
        public ParkingSpace ParkedIn { get; set; }
    }
}
