using P14_AI_Blog_Generator_Backend.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P14_AI_Blog_Generator_Backend.Configurations;

public class DeletedAccountConfiguration : IEntityTypeConfiguration<DeletedAccount>
{
    public void Configure(EntityTypeBuilder<DeletedAccount> builder)
    {
        builder.ToTable("DeletedAccounts");

        builder.HasKey(x => x.DeletedId);

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