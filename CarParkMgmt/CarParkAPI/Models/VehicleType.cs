namespace CarParkAPI.Models
{
    public enum VehicleType
    {
        SmallCar = 1,
        MediumCar = 2,
        LargeCar = 3
    }

    public static class VehicleTypeConverter
    {
        public static VehicleType ToVehicleType(int number)
        {
            return number switch
            {
                1 => VehicleType.SmallCar,
                2 => VehicleType.MediumCar,
                3 => VehicleType.LargeCar,
                _ => throw new NotImplementedException($"No mapping of {number} to {typeof(VehicleType)}.")
            };
        }
    }
}
