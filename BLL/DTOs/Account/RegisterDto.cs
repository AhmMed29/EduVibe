using System.ComponentModel.DataAnnotations;
using EduVibe.DTOs.Shared;
using EduVibe.Models.Enums;
using EduVibe.Validators;

namespace EduVibe.DTOs.Account;

public class RegisterDto
{
    [Required, StringLength(50)]
    public string Fname { get; set; } = string.Empty;
    
    [Required, StringLength(50)]
    public string Lname { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required, StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
    
    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    [Required]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone Number Must be 11 digits")]
    public string PhoneNumber { get; set; } =  string.Empty;
    
    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidateAge))]
    public DateOnly DateOfBirth { get; set; }
    
    [Required]
    public AddressDto Address { get; set; }
    
    [Required]
    public GenderType GenderType { get; set; }
    
    [Required]
    public string Role { get; set; } = "Student";
}
