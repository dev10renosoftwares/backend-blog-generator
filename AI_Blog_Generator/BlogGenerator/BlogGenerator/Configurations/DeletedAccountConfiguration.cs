using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogGenerator.Configurations;

public class DeletedAccountConfiguration : IEntityTypeConfiguration<DeletedAccount>
{
    public void Configure(EntityTypeBuilder<DeletedAccount> builder)
    {
        builder.ToTable("DeletedAccounts");

        builder.HasKey(x => x.DeletedId);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Reason)
            .HasMaxLength(500);

        builder.Property(x => x.DeletedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}