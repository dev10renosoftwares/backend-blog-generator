namespace BlogGenerator.DomainModels.v1;

public class Blog
{
    public int BlogId { get; set; }

    public int UserId { get; set; }
    public int CategoryId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public string Prompt { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
    public string? Excerpt { get; set; }
    public string Tone { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public int CreditsUsed { get; set; }
    public BlogLanguage Language { get; set; } = BlogLanguage.English;

    public BlogStatus Status { get; set; }

    public BlogVisibility Visibility { get; set; }

    public bool AllowComments { get; set; } = true;

    public int? ReadingTime { get; set; }

    public int ViewsCount { get; set; } = 0;

    public int LikesCount { get; set; } = 0;

    public int CommentsCount { get; set; } = 0;

    public int BookmarksCount { get; set; } = 0;

    public int RepostsCount { get; set; } = 0;
    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Category Category { get; set; } = null!;

    public ICollection<BlogVersion> BlogVersions { get; set; } = new List<BlogVersion>();

    public ICollection<BlogImage> BlogImages { get; set; } = new List<BlogImage>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();

    public ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();

    public ICollection<BlogReport> BlogReports { get; set; } = new List<BlogReport>();
}