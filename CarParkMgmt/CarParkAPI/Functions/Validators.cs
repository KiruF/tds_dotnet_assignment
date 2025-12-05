using System.Text;
using System.Text.RegularExpressions;

namespace CarParkAPI.Functions
{
    public readonly struct ValidationResult(bool isValid, string log)
    {
        public readonly bool IsValid = isValid;
        public readonly string Log = log;

        public static ValidationResult Invalid(string log)
            => new ValidationResult(false, log);

        public static ValidationResult Valid()
            => new ValidationResult(true, string.Empty);
    }

    public static class Validators
    {
        const string INVALID_REGEX_PATTERN = @"[^A-Z0-9]";

        public static ValidationResult ValidateVehicleReg(string reg)
        {
            MatchCollection invalidChars = Regex.Matches(reg, INVALID_REGEX_PATTERN);
            if (invalidChars.Count == 0)
                return ValidationResult.Valid();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("Invalid characters:");

            foreach (var invalidChar in invalidChars)
                strBuilder.AppendLine(invalidChar.ToString());

            return ValidationResult.Invalid(strBuilder.ToString());
        }
    }
}
