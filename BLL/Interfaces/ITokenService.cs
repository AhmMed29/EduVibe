using System.Security.Claims;
using EduVibe.Models.Entities;

namespace BLL.Interfaces;
public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(ApplicationUser user);
    Task<string> GenerateRefreshTokenAsync(ApplicationUser user);
    // this to ensure that th JWT was signed by us and hasn't been edited
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(ApplicationUser user);
}
