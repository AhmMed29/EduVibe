using System.ComponentModel.DataAnnotations;
using static System.DateTime;

namespace EduVibe.Validators
{
	public static class DateValidator
	{
		public static ValidationResult ValidateAge(DateOnly date, ValidationContext context)
		{
			if (date > DateOnly.FromDateTime(Now))
			{
				return new ValidationResult("Date of Birth cannot be in the future.");
			}

			return ValidationResult.Success!;
		}
	}
}