using EduVibe.Data;
using EduVibe.DTOs.Account;
using EduVibe.Models.Enums;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BLL.Services;

public class OtpService : IOtpService
{
    public readonly IEmailSender _emailSender;

    public OtpService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task<OtpResult> GenerateAsync(string email, OtpPurpose purpose)
    {
        // generating the code & sending to email
        int PlainCode = RandomNumberGenerator.GetInt32(100000, 1000000);

        await _emailSender.SendEmailAsync(email, "Code From EduVibe", $"Your verification code is: {PlainCode}");

        // hashing here        
        string hashedCode = HashCode(PlainCode.ToString());

        // returning the hashedCode [storing is in the AuthService]
        return new OtpResult
        {
            HashedCode = hashedCode
        };
    }

    public static string HashCode(string plainCode)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(plainCode));
        return Convert.ToBase64String(bytes);
    }
}
