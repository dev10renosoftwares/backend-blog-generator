using P14_AI_Blog_Generator_Backend.DTOs.Credit;

namespace P14_AI_Blog_Generator_Backend.Interfaces;

public interface ICreditService
{
    Task<GetCreditsResponseDto> GetAvailableCreditsAsync(int userId);

    Task<List<CreditPricingResponseDto>> GetPricingAsync();
}