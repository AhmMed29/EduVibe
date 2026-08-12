using EduVibe.Models.Entities;

namespace BLL.Interfaces;
public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(ApplicationUser user);
    Task<string> GenerateRefreshTokenAsync(ApplicationUser user);
    Task<string> GetPrincipleFromExpiredToken(ApplicationUser user);
}
