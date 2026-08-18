namespace BlogGenerator.ServiceModels.v1.Blog;

public class BlogResponseDto
{
    public int BlogId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string? Excerpt { get; set; }

    public string Tone { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}