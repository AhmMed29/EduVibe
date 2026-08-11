using BLL.Interfaces;
using EduVibe.DTOs.Account;
using EduVibe.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduVibe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto request)
    {
        if (request == null) return BadRequest();

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded) return Unauthorized();

        var token = _tokenService.GenerateAccessTokenAsync(user);
        return Ok(new { Token = token });
    }
}
