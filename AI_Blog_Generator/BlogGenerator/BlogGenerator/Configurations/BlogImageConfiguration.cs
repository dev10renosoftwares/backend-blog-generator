using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class BlogImageConfiguration : IEntityTypeConfiguration<BlogImage>
{
    public void Configure(EntityTypeBuilder<BlogImage> builder)
    {
        builder.ToTable("BlogImages");

        builder.HasKey(x => x.ImageId);

        builder.Property(x => x.BlogId)
            .IsRequired();

        builder.Property(x => x.Prompt)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ImageType)
            .IsRequired();

        builder.Property(x => x.DisplayOrder)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(x => x.CreditsUsed)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // Blog → BlogImages
        builder.HasOne(x => x.Blog)
            .WithMany(x => x.BlogImages)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}