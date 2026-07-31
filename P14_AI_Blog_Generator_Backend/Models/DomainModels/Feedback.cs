using P14_AI_Blog_Generator_Backend.Enums;

namespace P14_AI_Blog_Generator_Backend.Models.DomainModels;

public class Feedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public int Rating { get; set; }

    public bool IsPublic { get; set; } = false;

    public FeedbackStatus Status { get; set; }

    public string? AdminResponse { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;
}