using BlogGenerator.DomainModels.v1;
using BlogGenerator.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blogs");

        builder.HasKey(x => x.BlogId);

        // Foreign Keys
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .IsRequired();

        // Basic Properties
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Prompt)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.Excerpt)
            .HasMaxLength(500);

        builder.Property(x => x.Tone)
            .IsRequired();

        builder.Property(x => x.Audience)
            .IsRequired();

        builder.Property(x => x.WordCount)
            .IsRequired();

        builder.Property(x => x.CreditsUsed)
            .IsRequired();

        builder.Property(x => x.Language)
            .IsRequired()
            .HasDefaultValue(BlogLanguage.English);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Visibility)
            .IsRequired();

        builder.Property(x => x.AllowComments)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.ReadingTime);

        builder.Property(x => x.ViewsCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.LikesCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.CommentsCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.BookmarksCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.RepostsCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.PublishedAt);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        // Unique Slug
        builder.HasIndex(x => x.Slug)
            .IsUnique();

        // User → Blogs
        builder.HasOne(x => x.User)
            .WithMany(x => x.Blogs)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Category → Blogs
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Blogs)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Blog → BlogVersions
        builder.HasMany(x => x.BlogVersions)
            .WithOne(x => x.Blog)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        // Blog → BlogImages
        builder.HasMany(x => x.BlogImages)
            .WithOne(x => x.Blog)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}