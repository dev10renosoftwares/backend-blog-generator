namespace BlogGenerator.DomainModels.v1;

public class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}