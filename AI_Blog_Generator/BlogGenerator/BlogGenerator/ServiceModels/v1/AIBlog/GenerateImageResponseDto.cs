

namespace BlogGenerator.ServiceModels.v1.AIBlog;

public class GenerateImageResponseDto
{
    public int BlogId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}