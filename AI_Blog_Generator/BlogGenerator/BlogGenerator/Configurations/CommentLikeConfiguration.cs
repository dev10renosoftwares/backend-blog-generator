using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLikes>
{
    public void Configure(EntityTypeBuilder<CommentLikes> builder)
    {
        builder.ToTable("CommentLikes");

        builder.HasKey(x => x.CommentLikeId);

        builder.Property(x => x.CommentId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Comment)
            .WithMany()
            .HasForeignKey(x => x.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CommentId, x.UserId })
            .IsUnique();
    }
}