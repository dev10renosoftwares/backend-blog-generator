namespace BlogGenerator.ServiceModels.v1.Blog;

public class BlogVersionDto
{
    public int VersionId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string VersionType { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public DateTime CreatedAt { get; set; }
}