namespace BlogGenerator.ServiceModels.v1.Profile
{
    public class UserSearchDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
    }
}
