namespace P14_AI_Blog_Generator_Backend.DTOs.Blogs;

public class BlogListDto
{
    public int BlogId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}