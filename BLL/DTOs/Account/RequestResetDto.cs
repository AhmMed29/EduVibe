using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Account;

public class RequestResetDto
{
    [Required]
    public string Email { get; set; }
}