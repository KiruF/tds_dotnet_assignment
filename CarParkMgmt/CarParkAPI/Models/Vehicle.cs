using CarParkAPI.Controllers;
using CarParkAPI.Models.DTOs;

namespace CarParkAPI.Models
{
    public class Vehicle
    {
        public int ID { get; set; }

        public VehicleType VehicleType { get; set; }
        public string Reg { get; set; }
        public DateTime TimeIn { get; set; }

        public int ParkingSpaceID { get; set; }
        public ParkingSpace ParkingSpace { get; set; }

        public ParkedVehicle_Dto GetParkedInfo()
            => new ParkedVehicle_Dto
            {
                VehicleReg = Reg,
                SpaceNumber = ParkingSpace.SpaceNumber,
                TimeIn = TimeIn
            };

    }
}
