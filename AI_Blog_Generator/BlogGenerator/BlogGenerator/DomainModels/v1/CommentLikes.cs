using System;

namespace BlogGenerator.DomainModels.v1;

public class CommentLikes
{
    public int CommentLikeId { get; set; }

    public int CommentId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public Comments Comment { get; set; } = null!;

    public User User { get; set; } = null!;
}