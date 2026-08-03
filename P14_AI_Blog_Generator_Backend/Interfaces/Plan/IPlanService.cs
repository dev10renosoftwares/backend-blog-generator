using P14_AI_Blog_Generator_Backend.DTOs.Plan;

namespace P14_AI_Blog_Generator_Backend.Interfaces;

public interface IPlanService
{
    Task<List<PlanResponseDto>> GetAllPlansAsync();

    Task<PlanResponseDto> CreatePlanAsync(CreatePlanRequestDto request);

    Task<PlanResponseDto> UpdatePlanAsync(int planId, UpdatePlanRequestDto request);

    Task DeletePlanAsync(int planId);
}