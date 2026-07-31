using P14_AI_Blog_Generator_Backend.Enums;

namespace P14_AI_Blog_Generator_Backend.Models.DomainModels;

public class Payment
{
    public int PaymentId { get; set; }

    public int UserId { get; set; }

    public int PlanId { get; set; }

    public decimal Amount { get; set; }

    public int CreditsPurchased { get; set; }

    public string StripePaymentIntentId { get; set; } = string.Empty;

    public PaymentStatus PaymentStatus { get; set; }

    public DateTime PurchasedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;

    public Plan Plan { get; set; } = null!;
}