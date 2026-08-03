namespace BlogGenerator.DomainModels.v1;

public class DeletedAccount
{
    public int DeletedId { get; set; }

    public int UserId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? Reason { get; set; }

    public DateTime DeletedAt { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;
}