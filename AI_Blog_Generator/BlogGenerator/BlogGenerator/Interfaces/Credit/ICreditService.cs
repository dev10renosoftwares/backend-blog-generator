using BlogGenerator.ServiceModels.v1;

namespace BlogGenerator.Interfaces;

public interface ICreditService
{
    Task<GetCreditsResponseDto> GetAvailableCreditsAsync(int userId);

    Task<List<CreditPricingResponseDto>> GetPricingAsync();
}