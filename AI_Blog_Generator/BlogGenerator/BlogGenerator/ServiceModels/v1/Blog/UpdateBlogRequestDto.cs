namespace BlogGenerator.ServiceModels.v1.Blog;

public class UpdateBlogRequestDto
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string? Excerpt { get; set; }

    public string Tone { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public int WordCount { get; set; }
}