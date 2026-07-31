namespace P14_AI_Blog_Generator_Backend.DTOs.Blogs;

public class BlogDetailsDto
{
    public int BlogId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Prompt { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Tone { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public int CreditsUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}