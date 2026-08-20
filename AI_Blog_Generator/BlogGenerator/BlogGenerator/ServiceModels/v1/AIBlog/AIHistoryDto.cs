namespace BlogGenerator.ServiceModels.v1.AIBlog;

public class AIHistoryDto
{
    public int BlogVersionId { get; set; }

    public int BlogId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string? Operation { get; set; }

    public int CreditsUsed { get; set; }

    public DateTime CreatedAt { get; set; }
}