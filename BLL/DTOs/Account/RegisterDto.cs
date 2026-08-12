using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Account;

public class RegisterDto
{
    [Required, StringLength(50)]
    public string Fname { get; set; } = string.Empty;
    
    [Required, StringLength(50)]
    public string Lname { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    
    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    [Phone]
    public string PhoneNumber { get; set; } =  string.Empty;
    
    [Required]
    public string Role { get; set; } = "Student";
}
