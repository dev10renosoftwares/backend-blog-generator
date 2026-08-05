using BlogGenerator.Enums;

namespace BlogGenerator.DomainModels.v1;

public class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public int AvailableCredits { get; set; } = 100;

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation Properties
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public DeletedAccount? DeletedAccount { get; set; }
}