namespace BlogGenerator.ServiceModels.v1.Profile
{
    public class UserSuggestionDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public int FollowersCount { get; set; }
    }
}
