namespace P14_AI_Blog_Generator_Backend.Models.DomainModels;

public class RefreshToken
{
    public int TokenId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }

    public bool IsRevoked { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;
}