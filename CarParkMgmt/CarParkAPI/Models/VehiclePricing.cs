namespace CarParkAPI.Models
{
    public class VehiclePricing
    {
        public int ID { get; set; }

        public VehicleType VehicleType { get; set; }
        public double PoundsPerMinute { get; set; }
    }
}
