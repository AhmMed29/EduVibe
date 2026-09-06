using EduVibe.Models.Enums;

namespace BLL.Services;

public interface IOtpService
{
    public Task<string> GenerateAsync(string email, OtpPurpose purpose);
}