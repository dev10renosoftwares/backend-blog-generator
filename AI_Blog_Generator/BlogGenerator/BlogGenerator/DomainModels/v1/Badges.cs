namespace BlogGenerator.DomainModels.v1;

public class Badges
{
    public int BadgeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? IconUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public ICollection<UserBadges> UserBadges { get; set; } = new List<UserBadges>();
}