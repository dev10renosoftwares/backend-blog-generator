namespace BlogGenerator.DomainModels.v1;

public class BlogImage
{
    public int ImageId { get; set; }

    public int BlogId { get; set; }

    public string Prompt { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int CreditsUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public Blog Blog { get; set; } = null!;
}