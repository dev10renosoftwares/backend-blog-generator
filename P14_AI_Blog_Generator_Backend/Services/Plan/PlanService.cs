using Microsoft.EntityFrameworkCore;
using P14_AI_Blog_Generator_Backend.Data;
using P14_AI_Blog_Generator_Backend.DTOs.Plan;
using P14_AI_Blog_Generator_Backend.Interfaces;
using P14_AI_Blog_Generator_Backend.Models.DomainModels;

namespace P14_AI_Blog_Generator_Backend.Services;

public class PlanService : IPlanService
{
    private readonly ApplicationDbContext _context;

    public PlanService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlanResponseDto>> GetAllPlansAsync()
    {
        return await _context.Plans
            .OrderBy(p => p.Price)
            .Select(p => new PlanResponseDto
            {
                PlanId = p.PlanId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Credits = p.Credits,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<PlanResponseDto> CreatePlanAsync(CreatePlanRequestDto request)
    {
        var exists = await _context.Plans
            .AnyAsync(p => p.Name == request.Name);

        if (exists)
            throw new Exception("Plan with the same name already exists.");

        var plan = new Plan
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Credits = request.Credits,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        return new PlanResponseDto
        {
            PlanId = plan.PlanId,
            Name = plan.Name,
            Description = plan.Description,
            Price = plan.Price,
            Credits = plan.Credits,
            IsActive = plan.IsActive,
            CreatedAt = plan.CreatedAt
        };
    }

    public async Task<PlanResponseDto> UpdatePlanAsync(int planId, UpdatePlanRequestDto request)
    {
        var plan = await _context.Plans.FindAsync(planId);

        if (plan == null)
            throw new Exception("Plan not found.");

        var duplicate = await _context.Plans.AnyAsync(p =>
            p.PlanId != planId &&
            p.Name == request.Name);

        if (duplicate)
            throw new Exception("Another plan with the same name already exists.");

        plan.Name = request.Name;
        plan.Description = request.Description;
        plan.Price = request.Price;
        plan.Credits = request.Credits;
        plan.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        return new PlanResponseDto
        {
            PlanId = plan.PlanId,
            Name = plan.Name,
            Description = plan.Description,
            Price = plan.Price,
            Credits = plan.Credits,
            IsActive = plan.IsActive,
            CreatedAt = plan.CreatedAt
        };
    }

    public async Task DeletePlanAsync(int planId)
    {
        var plan = await _context.Plans.FindAsync(planId);

        if (plan == null)
            throw new Exception("Plan not found.");

        plan.IsActive = false;

        await _context.SaveChangesAsync();
    }
}