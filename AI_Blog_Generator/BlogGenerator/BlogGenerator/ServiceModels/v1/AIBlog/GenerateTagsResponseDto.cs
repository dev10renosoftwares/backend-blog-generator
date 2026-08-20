namespace BlogGenerator.ServiceModels.v1.AIBlog;

public class GenerateTagsResponseDto
{
    public int BlogId { get; set; }

    public List<string> Tags { get; set; } = new();
}