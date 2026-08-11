namespace EduVibe.DTOs.Account;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresIn { get; set; } = DateTime.UtcNow;
    public string UserInfo { get; set; } = string.Empty;
}
