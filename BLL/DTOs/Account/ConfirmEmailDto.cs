using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Account;

public class ConfirmEmailDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Code { get; set; }
}