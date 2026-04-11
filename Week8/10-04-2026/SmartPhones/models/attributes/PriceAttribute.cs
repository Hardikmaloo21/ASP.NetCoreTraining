using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SmartPhones.models.attributes
{
    public class PriceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext? validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Price is required.");
            }

            try
            {
                decimal price;

                switch (value)
                {
                    case decimal d:
                        price = d;
                        break;
                    case double db:
                        price = Convert.ToDecimal(db);
                        break;
                    case float f:
                        price = Convert.ToDecimal(f);
                        break;
                    case int i:
                        price = i;
                        break;
                    case long l:
                        price = l;
                        break;
                    case string s:
                        s = s.Trim();
                        if (string.IsNullOrEmpty(s)) return new ValidationResult(ErrorMessage ?? "Price is required.");
                        if (!decimal.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var parsed))
                        {
                            return new ValidationResult(ErrorMessage ?? "Price is not a valid number.");
                        }
                        price = parsed;
                        break;
                    default:
                        // Try to convert other numeric types
                        price = Convert.ToDecimal(value);
                        break;
                }

                if (price <= 0m)
                {
                    return new ValidationResult(ErrorMessage ?? "Price must be greater than zero.");
                }

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult(ErrorMessage ?? "Price is not a valid number.");
            }
        }
    }
}
