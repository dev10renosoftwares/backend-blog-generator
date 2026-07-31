using P14_AI_Blog_Generator_Backend.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P14_AI_Blog_Generator_Backend.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.PaymentId);

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CreditsPurchased)
            .IsRequired();

        builder.Property(x => x.StripePaymentIntentId)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.PaymentStatus)
            .IsRequired();

        builder.Property(x => x.PurchasedAt)
            .IsRequired();

        builder.HasOne(x => x.Plan)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}