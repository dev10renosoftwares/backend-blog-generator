namespace BlogGenerator.DomainModels.v1;

public class Comments
{
    public int CommentId { get; set; }

    public int BlogId { get; set; }

    public int UserId { get; set; }

    public int? ParentCommentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public bool IsEdited { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation Properties
    public Blog Blog { get; set; } = null!;

    public User User { get; set; } = null!;

    public Comments? ParentComment { get; set; }

    public ICollection<Comments> Replies { get; set; } = new List<Comments>();
}