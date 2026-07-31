namespace P14_AI_Blog_Generator_Backend.DTOs.Blogs;

public class BlogImageDto
{
    public int ImageId { get; set; }

    public string Prompt { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int CreditsUsed { get; set; }

    public DateTime CreatedAt { get; set; }
}