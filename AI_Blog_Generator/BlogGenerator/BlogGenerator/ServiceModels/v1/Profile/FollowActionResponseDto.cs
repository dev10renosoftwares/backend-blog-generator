
namespace BlogGenerator.ServiceModels.v1.Profile;

public class FollowActionResponseDto
{
    public int UserId { get; set; }
    public bool IsFollowing { get; set; }
    public string Message { get; set; } = string.Empty;
}