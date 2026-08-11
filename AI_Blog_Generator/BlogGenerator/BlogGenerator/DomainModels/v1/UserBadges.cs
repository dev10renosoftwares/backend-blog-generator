namespace BlogGenerator.DomainModels.v1;

public class UserBadge
{
    public int UserBadgeId { get; set; }

    public int UserId { get; set; }

    public int BadgeId { get; set; }

    public DateTime EarnedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;

    public Badges Badge { get; set; } = null!;
}