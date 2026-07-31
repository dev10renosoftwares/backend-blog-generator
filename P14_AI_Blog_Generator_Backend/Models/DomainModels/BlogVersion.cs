using P14_AI_Blog_Generator_Backend.Enums;


namespace P14_AI_Blog_Generator_Backend.Models.DomainModels;

public class BlogVersion
{
    public int VersionId { get; set; }

    public int BlogId { get; set; }

    public string Title { get; set; } = string.Empty;

    public VersionType VersionType { get; set; }

    public string Content { get; set; } = string.Empty;

    public int WordCount { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public Blog Blog { get; set; } = null!;
}