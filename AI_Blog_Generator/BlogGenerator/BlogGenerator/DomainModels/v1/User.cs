using BlogGenerator.Enums;

namespace BlogGenerator.DomainModels.v1;

public class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? Website { get; set; }
    public string? Location { get; set; }

    public UserRole Role { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public int AvailableCredits { get; set; } = 100;

    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastSeenAt { get; set; }

    // Navigation Properties
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>(); 
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>(); 
    public ICollection<Follow> Followers { get; set; } = new List<Follow>(); 
    public ICollection<Follow> Following { get; set; } = new List<Follow>();
    public ICollection<Notification> ReceivedNotifications { get; set; } = new List<Notification>();
    public ICollection<Notification> SentNotifications { get; set; } = new List<Notification>();
    public ICollection<BlogReport> BlogReports { get; set; } = new List<BlogReport>();
    public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();


    public DeletedAccount? DeletedAccount { get; set; }
}