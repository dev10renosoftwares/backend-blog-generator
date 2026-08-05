using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class BlogVersionConfiguration : IEntityTypeConfiguration<BlogVersion>
{
    public void Configure(EntityTypeBuilder<BlogVersion> builder)
    {
        builder.ToTable("BlogVersions");

        builder.HasKey(x => x.VersionId);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.VersionType)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.WordCount)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}