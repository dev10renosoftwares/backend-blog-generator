namespace BlogGenerator.ServiceModels.v1.Profile
{
    public class UserActivityDto
    {
        public string ActivityType { get; set; } = string.Empty;
        public int? BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
