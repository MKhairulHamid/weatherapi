// WeatherApp.Core/Models/Validation/ValidationAttributes.cs
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Core.Models.Validation
{
    public class NonEmptyStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string stringValue)
            {
                return !string.IsNullOrWhiteSpace(stringValue);
            }
            return false;
        }
    }
}