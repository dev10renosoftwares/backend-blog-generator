using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.UserId);

        // Basic Properties
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.DisplayName)
            .HasMaxLength(100);

        builder.Property(x => x.Bio)
            .HasMaxLength(500);

        builder.Property(x => x.Website)
            .HasMaxLength(255);

        builder.Property(x => x.Location)
            .HasMaxLength(150);

        builder.Property(x => x.Role)
            .IsRequired();

        builder.Property(x => x.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(x => x.AvailableCredits)
            .IsRequired()
            .HasDefaultValue(100);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.LastSeenAt);

        // Unique Indexes
        builder.HasIndex(x => x.UserName)
            .IsUnique();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        // Refresh Tokens
        builder.HasMany(x => x.RefreshTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Blogs
        builder.HasMany(x => x.Blogs)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Payments
        builder.HasMany(x => x.Payments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Feedbacks
        builder.HasMany(x => x.Feedbacks)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Issues
        builder.HasMany(x => x.Issues)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Likes
        builder.HasMany(x => x.Likes)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Comments
        builder.HasMany(x => x.Comments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Bookmarks
        builder.HasMany(x => x.Bookmarks)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Reposts
        builder.HasMany(x => x.Reposts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Followers
        builder.HasMany(x => x.Followers)
            .WithOne(x => x.Follower)
            .HasForeignKey(x => x.FollowerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Following
        builder.HasMany(x => x.Following)
            .WithOne(x => x.Following)
            .HasForeignKey(x => x.FollowingUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Received Notifications
        builder.HasMany(x => x.ReceivedNotifications)
            .WithOne(x => x.ReceiverUser)
            .HasForeignKey(x => x.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Sent Notifications
        builder.HasMany(x => x.SentNotifications)
            .WithOne(x => x.SenderUser)
            .HasForeignKey(x => x.SenderUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Blog Reports
        builder.HasMany(x => x.BlogReports)
            .WithOne(x => x.ReportedByUser)
            .HasForeignKey(x => x.ReportedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // User Badges
        builder.HasMany(x => x.UserBadges)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}