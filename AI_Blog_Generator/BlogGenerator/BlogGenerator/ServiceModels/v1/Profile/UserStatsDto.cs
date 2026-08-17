namespace BlogGenerator.ServiceModels.v1.Profile
{
    public class UserStatsDto
    {
        public int UserId { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int BlogsCount { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int RepostsCount { get; set; }
        public int BookmarksCount { get; set; }
        public int PublishedBlogsCount { get; set; }
        public int ViewsCount { get; set; }
    }
}
