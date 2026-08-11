namespace BlogGenerator.DomainModels.v1;

public class Tags
{
    public int TagId { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public ICollection<BlogTags> BlogTags { get; set; } = new List<BlogTags>();
}