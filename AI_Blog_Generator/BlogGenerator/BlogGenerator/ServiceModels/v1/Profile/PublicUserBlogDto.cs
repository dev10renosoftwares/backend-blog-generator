
namespace BlogGenerator.ServiceModels.v1.Profile;

public class PublicUserBlogDto
{
    public int BlogId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime PublishedAt { get; set; }
}