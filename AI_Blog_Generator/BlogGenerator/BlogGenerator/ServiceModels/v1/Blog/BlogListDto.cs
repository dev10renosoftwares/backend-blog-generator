namespace BlogGenerator.ServiceModels.v1.Blog;

public class BlogListDto
{
    public int BlogId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}