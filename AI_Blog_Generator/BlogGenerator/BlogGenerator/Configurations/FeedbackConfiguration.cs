using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");

        builder.HasKey(x => x.FeedbackId);

        builder.Property(x => x.Subject)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Message)
            .IsRequired();

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.Property(x => x.IsPublic)
            .HasDefaultValue(false);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.AdminResponse);

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}