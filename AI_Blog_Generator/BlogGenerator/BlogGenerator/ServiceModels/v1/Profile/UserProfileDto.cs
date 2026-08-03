namespace BlogGenerator.ServiceModels.v1.Profile;

public class UserProfileDto
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; }

    public int AvailableCredits { get; set; }

    public string Role { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}