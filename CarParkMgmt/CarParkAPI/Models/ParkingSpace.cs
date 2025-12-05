
namespace CarParkAPI.Models
{
    public class ParkingSpace
    {
        public int ID { get; set; }

        public int SpaceNumber { get; set; }
        public Vehicle? ParkedVehicle { get; set; }
    }
}
