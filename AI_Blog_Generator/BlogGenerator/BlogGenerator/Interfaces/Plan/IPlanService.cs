using BlogGenerator.ServiceModels.v1;

namespace BlogGenerator.Interfaces;

public interface IPlanService
{
    Task<List<PlanResponseDto>> GetAllPlansAsync();

    Task<PlanResponseDto> CreatePlanAsync(CreatePlanRequestDto request);

    Task<PlanResponseDto> UpdatePlanAsync(int planId, UpdatePlanRequestDto request);

    Task DeletePlanAsync(int planId);
}