using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.ToTable("Follows");

        builder.HasKey(x => x.FollowId);

        builder.Property(x => x.FollowerUserId)
            .IsRequired();

        builder.Property(x => x.FollowingUserId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Follower)
            .WithMany()
            .HasForeignKey(x => x.FollowerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Following)
            .WithMany()
            .HasForeignKey(x => x.FollowingUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.FollowerUserId, x.FollowingUserId })
            .IsUnique();
    }
}