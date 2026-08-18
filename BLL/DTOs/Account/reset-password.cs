using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Account;

public class reset_password
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
}