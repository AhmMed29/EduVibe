using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Account;

public class ResendCodeDto
{
    [Required]
    public string Email { get; set; }
}