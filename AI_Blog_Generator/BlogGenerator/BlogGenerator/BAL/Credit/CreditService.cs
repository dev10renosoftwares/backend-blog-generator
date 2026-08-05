using Microsoft.EntityFrameworkCore;
using BlogGenerator.DAL;
using BlogGenerator.Foundation.Exceptions;
using BlogGenerator.Interfaces;
using BlogGenerator.ServiceModels.v1;

namespace BlogGenerator.Services;

public class CreditService : ICreditService
{
    private readonly ApplicationDbContext _context;

    public CreditService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetCreditsResponseDto> GetAvailableCreditsAsync(int userId)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);

        if (user == null)
            throw new NotFoundException("User not found.");

        return new GetCreditsResponseDto
        {
            AvailableCredits = user.AvailableCredits
        };
    }

    public Task<List<CreditPricingResponseDto>> GetPricingAsync()
    {
        var pricing = new List<CreditPricingResponseDto>
        {
            new CreditPricingResponseDto
            {
                ServiceName = "Generate Blog",
                CreditsRequired = 10,
                Description = "Generate a new AI blog."
            },
            new CreditPricingResponseDto
            {
                ServiceName = "Generate Image",
                CreditsRequired = 5,
                Description = "Generate an AI image."
            },
            new CreditPricingResponseDto
            {
                ServiceName = "Regenerate Blog",
                CreditsRequired = 5,
                Description = "Generate another version of an existing blog."
            },
            new CreditPricingResponseDto
            {
                ServiceName = "Expand Blog",
                CreditsRequired = 3,
                Description = "Expand the existing blog."
            },
            new CreditPricingResponseDto
            {
                ServiceName = "Shorten Blog",
                CreditsRequired = 2,
                Description = "Shorten the existing blog."
            }
        };

        return Task.FromResult(pricing);
    }
}