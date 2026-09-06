using EduVibe.DTOs.Account;
using EduVibe.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces;

public interface IAuthService
{
    // using Task<> means that it will return something in the future 
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    
    // using Task means that it will NOT return anything
    Task RequestResetAsync(RequestResetDto dto);
    Task ConfirmResetAsync(ConfirmResetDto dto);

    Task ConfirmEmailAsync(ConfirmEmailDto dto);
    Task ResendConfirmationAsync(ResendCodeDto dto);
}