namespace BlogGenerator.DomainModels.v1;

public class Reposts
{
    public int RepostId { get; set; }

    public int BlogId { get; set; }

    public int UserId { get; set; }

    public string? Caption { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public Blog Blog { get; set; } = null!;

    public User User { get; set; } = null!;
}