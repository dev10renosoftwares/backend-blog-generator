namespace BlogGenerator.ServiceModels.v1.Authentication;

public class AuthResponseDto
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public int AvailableCredits { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime ExpiresAt { get; set; }
}