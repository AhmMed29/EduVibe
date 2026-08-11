using System.ComponentModel.DataAnnotations;

namespace EduVibe.Validators
{
	public static class DateValidator
	{
		public static ValidationResult ValidateAge(DateTime date, ValidationContext context)
		{
			if (date > DateTime.Now)
			{
				return new ValidationResult("Date of Birth cannot be in the future.");
			}

			if (DateTime.Now.Year - date.Year < 18)
			{
				return new ValidationResult("Student must be at least 18 years old.");
			}

			return ValidationResult.Success!;
		}
}
}