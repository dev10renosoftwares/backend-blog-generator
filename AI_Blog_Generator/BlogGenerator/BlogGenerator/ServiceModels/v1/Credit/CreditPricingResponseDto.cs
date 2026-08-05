

namespace BlogGenerator.ServiceModels.v1;

public class CreditPricingResponseDto
{
    public string ServiceName { get; set; } = string.Empty;

    public int CreditsRequired { get; set; }

    public string Description { get; set; } = string.Empty;
}