namespace BlogGenerator.DomainModels.v1;

public class Plan
{
    public int PlanId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Credits { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}