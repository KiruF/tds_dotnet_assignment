using CarParkAPI.Models;

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
                new VehiclePricing{ VehicleType= VehicleType.SmallCar, PoundsPerMinute=0.1 },
                new VehiclePricing{ VehicleType = VehicleType.MediumCar, PoundsPerMinute =  0.2 },
                new VehiclePricing{ VehicleType = VehicleType.LargeCar, PoundsPerMinute = 0.4 }
            };
            context.Pricing.AddRange(pricing);

            // Seed parking spaces
            //TODO: fetch from somewhere
            int parkingSpacesCount = 10;
            double freeSpacesChance = 0.5;
            var rnd = new Random();
            var parkingSpaces = new ParkingSpace[parkingSpacesCount];
            for (int i = 0; i < parkingSpacesCount; i++)
            {
                var pSpace = new ParkingSpace { SpaceNumber = i };

                bool isFree = rnd.NextDouble() < freeSpacesChance;
                if (!isFree)
                {
                    DateTime yesterday = DateTime.Today.AddDays(-1);

                    var vehicle = new Vehicle
                    {
                        VehicleType = VehicleType.MediumCar,
                        Reg = $"Hello{i}",
                        TimeIn = yesterday,
                        ParkedIn = pSpace
                    };

                    pSpace.ParkedVehicle = vehicle;
                }

                parkingSpaces[i] = pSpace;
            }
            context.ParkingSpaces.AddRange(parkingSpaces);

            context.SaveChanges();
        }
    }
}
