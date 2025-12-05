using CarParkAPI.Models;
using static CarParkAPI.Functions.ChargeCalculator;

namespace UnitTests
{
    public class ChargeCalculator_UnitTests
    {
        Dictionary<VehicleType, double> _invalidPricing;

        public ChargeCalculator_UnitTests()
        {
            _invalidPricing = new Dictionary<VehicleType, double>
            {
                { VehicleType.MediumCar , -10},
                { VehicleType.SmallCar, 3.14 }
            };
        }

        [Theory]
        [InlineData(-1)]
        [InlineData((int)VehicleType.MediumCar)]
        public void TryCharge_InvalidPricing_ShouldReturnFalse(int vehicleTypeInt)
        {
            // Arrange
            var vehicle = new Vehicle { VehicleType = (VehicleType)vehicleTypeInt };
            // Act
            bool res = TryCharge(vehicle, _invalidPricing, out _);
            // Assert
            Assert.False(res);
        }
    }
}