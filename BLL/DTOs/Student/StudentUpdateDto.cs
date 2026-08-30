using System.ComponentModel.DataAnnotations;
using EduVibe.Models.Entities;
using EduVibe.Models.Enums;
using EduVibe.Validators;

namespace EduVibe.DTOs.Student;

public class StudentUpdateDto
{
    [Required]
    [StringLength(50, ErrorMessage = "First Name Must be less Than 20 Letter")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50, ErrorMessage = "Last Name Must be less Than 20 Letter")]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone Number Must be 11 digits")]
    public string Phone { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidateAge))]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public GenderType Gender { get; set; }
    
    [Required]
    public AddressDto Address { get; set; }
}