using BLL.Interfaces;
using BLL.Settings;
using EduVibe.DTOs.Account;
using EduVibe.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService; 
    private readonly IOptions<JwtSettings> _jwtSettings;
    private static readonly string[] AllowedPublicRoles = { "Student", "Instructor" };

    public AuthService(UserManager<ApplicationUser> userManager
        , ITokenService tokenService
        , IOptions<JwtSettings> jwtSettings)   
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("Email is already registered.");

        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.Fname,
            LastName = dto.Lname,
            PhoneNumber = dto.PhoneNumber
        };
        
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(x => x.Description));
            throw new Exception($"Registration  Failed: {errors}");
        }
        
        var requestedRole = dto.Role?.Trim() ?? "Student";
        if (!AllowedPublicRoles.Contains(requestedRole))
            throw new Exception($"Role '{requestedRole}' is not allowed for self-registration.");
        await _userManager.AddToRoleAsync(user, requestedRole);
        
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateAccessTokenAsync(user);
        
        return new AuthResponseDto
        {
            AccessToken = await token,
            ExpiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.DurationInMinutes),
            Email = user.Email,
            Roles = roles.ToList(),
            UserName = user.UserName
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateAccessTokenAsync(user);

        return new AuthResponseDto
        {
            AccessToken = await token,
            ExpiresIn = DateTime.UtcNow.AddMinutes(60),
            Email = user.Email,
            UserName = user.UserName,
            Roles = roles.ToList()
        };
    }
}