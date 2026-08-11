using BLL.Interfaces;                          
using BLL.Settings;                            
using Microsoft.AspNetCore.Identity;           
using Microsoft.Extensions.Options;            
using EduVibe.Models.Entities; 

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, ITokenService tokenService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        
    }
}