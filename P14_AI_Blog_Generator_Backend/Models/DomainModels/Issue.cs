using P14_AI_Blog_Generator_Backend.Enums;

namespace P14_AI_Blog_Generator_Backend.Models.DomainModels;

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