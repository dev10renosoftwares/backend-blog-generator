namespace BlogGenerator.DomainModels.v1;

public class Blog
{
    public int BlogId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Prompt { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Tone { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public int CreditsUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;

    public ICollection<BlogVersion> BlogVersions { get; set; } = new List<BlogVersion>();

    public ICollection<BlogImage> BlogImages { get; set; } = new List<BlogImage>();
}