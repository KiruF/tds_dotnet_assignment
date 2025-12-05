using CarParkAPI.Models;
using CarParkAPI.Models.DTOs;
using static CarParkAPI.Data.ParkingSettings;

namespace CarParkAPI.Functions
{
    public class ChargeCalculator
    {
        public static bool TryCharge(
            Vehicle vehicle,
            Dictionary<VehicleType, double> pricing,
            out ParkingCharge_Dto charge)
        {
            charge = default!;
            var timeOut = DateTime.Now;

            if (!pricing.TryGetValue(vehicle.VehicleType, out var poundsPerMinute))
                return false;

            var minutesSpent = (timeOut - vehicle.TimeIn).TotalMinutes;
            var additionalChargeCount = Math.Floor(
                minutesSpent / AdditionalCharge_TimeSpanMinutes);

            var poundsCharged =
                minutesSpent * poundsPerMinute +
                additionalChargeCount * AdditionalCharge_PricePounds;

            charge = new ParkingCharge_Dto
            {
                VehicleReg = vehicle.Reg,
                VehicleCharge = poundsCharged,
                TimeIn = vehicle.TimeIn,
                TimeOut = timeOut
            };
            return true;
        }
    }
}
