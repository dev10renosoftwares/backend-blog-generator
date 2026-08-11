namespace BlogGenerator.DomainModels.v1;

public class Follow
{
    public int FollowId { get; set; }

    public int FollowerUserId { get; set; }

    public int FollowingUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public User Follower { get; set; } = null!;

    public User Following { get; set; } = null!;
}