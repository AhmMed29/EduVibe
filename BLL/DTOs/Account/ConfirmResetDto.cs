using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Account;

public class ConfirmResetDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string NewPassword { get; set; }
    
    [Required]
    public string Token { get; set; }
}