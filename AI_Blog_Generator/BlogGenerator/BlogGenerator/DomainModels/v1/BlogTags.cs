using Azure;

namespace BlogGenerator.DomainModels.v1;

public class BlogTags
{
    public int BlogTagId { get; set; }

    public int BlogId { get; set; }

    public int TagId { get; set; }

    // Navigation Properties
    public Blog Blog { get; set; } = null!;

    public Tags Tag { get; set; } = null!;
}