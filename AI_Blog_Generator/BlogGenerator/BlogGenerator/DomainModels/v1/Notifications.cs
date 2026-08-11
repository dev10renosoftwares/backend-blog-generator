using BlogGenerator.Enums;

namespace BlogGenerator.DomainModels.v1;

public class Notifications
{
    public int NotificationId { get; set; }

    public int ReceiverUserId { get; set; }

    public int? SenderUserId { get; set; }

    public int? BlogId { get; set; }

    public int? CommentId { get; set; }

    public NotificationType NotificationType { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public User ReceiverUser { get; set; } = null!;

    public User? SenderUser { get; set; }

    public Blog? Blog { get; set; }

    public Comments? Comment { get; set; }
}