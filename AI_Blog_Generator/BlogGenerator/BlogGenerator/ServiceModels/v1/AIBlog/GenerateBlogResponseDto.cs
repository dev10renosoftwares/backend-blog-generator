namespace BlogGenerator.ServiceModels.v1.AIBlog;

public class GenerateBlogResponseDto
{
    public int BlogId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string? Excerpt { get; set; }

    public int CreditsUsed { get; set; }

    public int RemainingCredits { get; set; }
}