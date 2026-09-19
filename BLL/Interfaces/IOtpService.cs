using EduVibe.DTOs.Account;
using EduVibe.Models.Enums;

namespace BLL.Services;

public interface IOtpService
{
    public Task<OtpResult> GenerateAsync(string email, OtpPurpose purpose);
}