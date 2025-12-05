using CarParkAPI.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace CarParkAPI.Functions
{
    public readonly struct ValidationResult(bool isValid, string log)
    {
        public readonly bool IsValid = isValid;
        public readonly string Log = log;

        public static ValidationResult GetInvalid(string log)
            => new ValidationResult(false, log);

        public static ValidationResult GetValid()
            => new ValidationResult(true, string.Empty);
    }

    public static class Validators
    {
        const string INVALID_REGEX_PATTERN = @"[^A-Za-z0-9]";

        public static ValidationResult ValidateVehicleReg(string reg)
        {
            if (string.IsNullOrEmpty(reg))
                return ValidationResult.GetInvalid("Can't be neither null, nor empty.");

            MatchCollection invalidChars = Regex.Matches(reg, INVALID_REGEX_PATTERN);
            if (invalidChars.Count == 0)
                return ValidationResult.GetValid();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("Invalid characters:");

            foreach (var invalidChar in invalidChars)
                strBuilder.AppendLine(invalidChar.ToString());

            return ValidationResult.GetInvalid(strBuilder.ToString());
        }

        public static ValidationResult ValidateVehicleType(int vehicleType, out VehicleType vType)
        {
            vType = default;
            try
            {
                vType = VehicleTypeConverter.ToVehicleType(vehicleType);
                return ValidationResult.GetValid();
            }
            catch (Exception ex)
            {
                return ValidationResult.GetInvalid(ex.Message);
            }
        }
    }
}
