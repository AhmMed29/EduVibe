using System.ComponentModel.DataAnnotations;
using EduVibe.Validators;
using EduVibe.DTOs.Shared;

namespace EduVibe.DTOs.Instructor;

public class InstructorUpdateDto
{
    [Required]
    [StringLength(20, ErrorMessage = "First Name must be less than 20 characters")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(20, ErrorMessage = "Last Name must be less than 20 characters")]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone Number must be 11 digits")]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidateAge))]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public AddressDto Address { get; set; } = null!;

    [Range(0, 1000000, ErrorMessage = "Salary must be a positive value")]
    public decimal? Salary { get; set; }

    public int? DepartmentId { get; set; }
}
