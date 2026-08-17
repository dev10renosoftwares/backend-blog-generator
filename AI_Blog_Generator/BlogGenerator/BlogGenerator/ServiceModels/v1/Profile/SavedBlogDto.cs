namespace BlogGenerator.ServiceModels.v1.Profile;

public class SavedBlogDto
{
    public int BlogId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime? PublishedAt { get; set; }
}