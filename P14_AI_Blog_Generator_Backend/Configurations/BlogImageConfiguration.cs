using P14_AI_Blog_Generator_Backend.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P14_AI_Blog_Generator_Backend.Configurations;

public class BlogImageConfiguration : IEntityTypeConfiguration<BlogImage>
{
    public void Configure(EntityTypeBuilder<BlogImage> builder)
    {
        builder.ToTable("BlogImages");

        builder.HasKey(x => x.ImageId);

        builder.Property(x => x.Prompt)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CreditsUsed)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}