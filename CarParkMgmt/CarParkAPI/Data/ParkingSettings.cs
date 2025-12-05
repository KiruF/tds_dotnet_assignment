
namespace CarParkAPI.Data
{
    public static class ParkingSettings
    {
        public static int ParkingSpacesCount { get; set; }
        public static double DBInit_FreeSpaceChanceCoeff { get; set; }
        public static int AdditionalCharge_TimeSpanMinutes { get; set; }
        public static double AdditionalCharge_PricePounds { get; set; }

        public static void Load(ConfigurationManager configuration)
        {
            var section = configuration.GetSection(nameof(ParkingSettings));

            ParkingSpacesCount = section.GetValue<int>(nameof(ParkingSpacesCount));
            AdditionalCharge_TimeSpanMinutes = section.GetValue<int>(nameof(AdditionalCharge_TimeSpanMinutes));
            AdditionalCharge_PricePounds = section.GetValue<double>(nameof(AdditionalCharge_PricePounds));
        }
    }
}
