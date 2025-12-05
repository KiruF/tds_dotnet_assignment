using CarParkAPI.Models;
using static CarParkAPI.Data.ParkingSettings;

namespace CarParkAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed pricing
            var pricing = new[]
            {
                new VehiclePricing{ VehicleType= (int)VehicleType.SmallCar, PoundsPerMinute=0.1 },
                new VehiclePricing{ VehicleType = (int)VehicleType.MediumCar, PoundsPerMinute =  0.2 },
                new VehiclePricing{ VehicleType = (int)VehicleType.LargeCar, PoundsPerMinute = 0.4 }
            };
            context.Pricing.AddRange(pricing);

            // Seed parking spaces            
            int parkingSpacesCount = ParkingSpacesCount;
            double freeSpacesChance = DBInit_FreeSpaceChanceCoeff;

            var rnd = new Random();

            var parkingSpaces = new ParkingSpace[parkingSpacesCount];
            var vehicles = new List<Vehicle>(parkingSpacesCount);
            for (int i = 0; i < parkingSpacesCount; i++)
            {
                var pSpace = new ParkingSpace { SpaceNumber = i };

                bool isFree = rnd.NextDouble() < freeSpacesChance;
                if (!isFree)
                {
                    DateTime yesterday = DateTime.Today.AddDays(-1);

                    var fakeReg = $"HELLO{i}";

                    var vehicle = new Vehicle
                    {
                        VehicleType = VehicleType.MediumCar,
                        Reg = fakeReg,
                        TimeIn = yesterday,
                        ParkingSpace = pSpace
                    };

                    vehicles.Add(vehicle);
                    pSpace.ParkedVehicle = vehicle;
                }

                parkingSpaces[i] = pSpace;
            }
            context.ParkingSpaces.AddRange(parkingSpaces);

            if (vehicles.Count != 0)
                context.Vehicles.AddRange(vehicles);

            context.SaveChanges();
        }
    }
}
