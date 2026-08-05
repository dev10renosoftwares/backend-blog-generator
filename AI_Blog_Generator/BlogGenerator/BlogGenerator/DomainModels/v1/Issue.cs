using BlogGenerator.Enums;

namespace BlogGenerator.DomainModels.v1;

public class Issue
{
    public int IssueId { get; set; }

    public int UserId { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IssueStatus Status { get; set; }

    public string? AdminResponse { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;
}