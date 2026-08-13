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
    public ICollection<Likes> Likes { get; set; } = new List<Likes>();
    public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    public ICollection<Bookmarks> Bookmarks { get; set; } = new List<Bookmarks>(); 
    public ICollection<Reposts> Reposts { get; set; } = new List<Reposts>(); 
    public ICollection<Follow> Followers { get; set; } = new List<Follow>(); 
    public ICollection<Follow> Following { get; set; } = new List<Follow>();
    public ICollection<Notifications> ReceivedNotifications { get; set; } = new List<Notifications>();
    public ICollection<Notifications> SentNotifications { get; set; } = new List<Notifications>();
    public ICollection<BlogReports> BlogReports { get; set; } = new List<BlogReports>();
    public ICollection<UserBadges> UserBadges { get; set; } = new List<UserBadges>();


    public DeletedAccount? DeletedAccount { get; set; }
}