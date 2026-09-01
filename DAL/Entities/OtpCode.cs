// this class to create an OTP Code to send to user email 
// after Login & register and password forget like : 486247

using System.ComponentModel.DataAnnotations;
using EduVibe.Models.Enums;

namespace EduVibe.Models.Entities;
public class OtpCode
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string CodeHash { get; set; } // store the otp code hashed
    public OtpPurpose Purpose;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public int Attempts { get; set; }
}