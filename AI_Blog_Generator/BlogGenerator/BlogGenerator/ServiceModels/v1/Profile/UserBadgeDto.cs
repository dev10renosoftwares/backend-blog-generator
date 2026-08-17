namespace BlogGenerator.ServiceModels.v1.Profile
{
    public class UserBadgeDto
    {
        public int BadgeId { get; set; }
        public string BadgeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public DateTime EarnedAt { get; set; }
    }
}
