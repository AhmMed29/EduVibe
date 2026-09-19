using System.Security.Cryptography;
using System.Text;
using BLL.Interfaces;
using BLL.Settings;
using EduVibe.Data;
using EduVibe.DTOs.Account;
using EduVibe.Models.Entities;
using EduVibe.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly IEmailSender _emailSender;
    private static readonly string[] AllowedPublicRoles = { "Student", "Instructor" };
    private readonly AppDbContext _context;
    private readonly IOtpService _otpService;

    public AuthService(UserManager<ApplicationUser> userManager
        , ITokenService tokenService
        , IOptions<JwtSettings> jwtSettings
        , IEmailSender emailSender
        , AppDbContext context
        , IOtpService otpService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings;
        _emailSender = emailSender;
        _context = context;
        _otpService = otpService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        // check role
        var requestedRole = dto.Role?.Trim() ?? "Student";
        if (!AllowedPublicRoles.Any(r => r.Equals(requestedRole, StringComparison.OrdinalIgnoreCase)))
            throw new Exception($"Invalid role.");

        // normalizing casing .. Aa
        requestedRole = AllowedPublicRoles
            .First(r => r.Equals(requestedRole, StringComparison.OrdinalIgnoreCase));

        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("Email is already registered.");

        // this transaction to implement the principle of (Unit Of Work): All or No One
        // "await using" : for unhandled exceptions it do the same what (catch) do 
        await using var transaction = _context.Database.BeginTransaction();
        try
        {
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


            if (requestedRole == "Student")
            {
                var student = new Student
                {
                    Fname = dto.Fname,
                    Lname = dto.Lname,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.GenderType,
                    Address = new StuAddress
                    {
                        City = dto.Address.City,
                        Country = dto.Address.Country
                    },
                    CreatedAt = DateTime.UtcNow,
                    ApplicationUserId = user.Id
                };

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }
            else if (requestedRole == "Instructor")
            {
                var instructor = new Instructor
                {
                    Fname = dto.Fname,
                    Lname = dto.Lname,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.GenderType,
                    Address = new InsAddress
                    {
                        City = dto.Address.City,
                        Country = dto.Address.Country
                    },
                    CreatedAt = DateTime.UtcNow,
                    ApplicationUserId = user.Id
                };

                await _context.Instructors.AddAsync(instructor);
                await _context.SaveChangesAsync();
            }

            await _userManager.AddToRoleAsync(user, requestedRole);

            var otpResult = await _otpService.GenerateAsync(dto.Email, OtpPurpose.Register);
            _context.OtpCodes.Add(new OtpCode
            {
                Email = dto.Email,
                CodeHash = otpResult.HashedCode,
                CreatedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddMinutes(5),
                Purpose = OtpPurpose.Register
            });
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenService.GenerateAccessTokenAsync(user);

            return new AuthResponseDto
            {
                AccessToken = token,
                ExpiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.DurationInMinutes),
                Email = user.Email,
                Roles = roles.ToList(),
                UserName = user.UserName
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw; // re-throw so the controller still returns the error
            // means: "I handled what I needed to (the rollback), now pass the same exception UP to the caller as if I never caught it."
            // With throw;, the exception continues up to the controller's catch (Exception ex) block, which returns the error in response.
        }
    }

    
    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        // email
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");
        
        // password
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var roles = await _userManager.GetRolesAsync(user);
        var token = await _tokenService.GenerateAccessTokenAsync(user);

        return new AuthResponseDto
        {
            AccessToken = token,
            ExpiresIn = DateTime.UtcNow.AddMinutes(60),
            Email = user.Email,
            UserName = user.UserName,
            Roles = roles.ToList()
        };
    }
    
    // Remember : TASK return NOTHING ! VOID.
    public async Task RequestResetAsync(RequestResetDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Email Not Found Try Entering it again.");
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        // subject : the (Title) Of the EMAIL MESSAGE 
        await _emailSender.SendEmailAsync(dto.Email, "Reset Password Request", $"Use This Token {token} To Reset YOur Password.");
    }

    public async Task ConfirmResetAsync(ConfirmResetDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Email Not Found.");
        
        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(x => x.Description));
            throw new Exception($"Password reset Failed: {errors}");
        }
    }

    // i will not do this ... No Perfectionism
    // public Task ConfirmEmailAsync(ConfirmEmailDto missing_name)
    // {
    //     throw new NotImplementedException();
    // }
    // public Task ResendConfirmationAsync(ResendCodeDto missing_name)
    // {
    //     throw new NotImplementedException();
    // }
}