

namespace P14_AI_Blog_Generator_Backend.DTOs.Credit;

public class CreditPricingResponseDto
{
    public string ServiceName { get; set; } = string.Empty;

    public int CreditsRequired { get; set; }

    public string Description { get; set; } = string.Empty;
}