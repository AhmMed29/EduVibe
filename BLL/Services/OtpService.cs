using System.Text;
using EduVibe.Models.Enums;
using System.Security.Cryptography;
using EduVibe.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BLL.Services;

public class OtpService : IOtpService
{
    public readonly AppDbContext _context;
    public readonly IEmailSender _emailSender;
    
    public OtpService (
            AppDbContext context ,
            IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }
    
    public async Task<string> GenerateAsync(string email, OtpPurpose purpose)
    {
        // generating the code
        int PlainCode = RandomNumberGenerator.GetInt32(100000, 1000000);
        
        // send plain code to email
        await _emailSender.SendEmailAsync(email, "Code From EduVibe",$"use this code {PlainCode}");
        return PlainCode.ToString();
    }
}