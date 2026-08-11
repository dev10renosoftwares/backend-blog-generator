using BlogGenerator.Enums;

namespace BlogGenerator.DomainModels.v1;

public class BlogReports
{
    public int ReportId { get; set; }

    public int BlogId { get; set; }

    public int ReportedByUserId { get; set; }

    public ReportReason Reason { get; set; }

    public string? Description { get; set; }

    public ReportStatus ReportStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public Blog Blog { get; set; } = null!;

    public User ReportedByUser { get; set; } = null!;
}