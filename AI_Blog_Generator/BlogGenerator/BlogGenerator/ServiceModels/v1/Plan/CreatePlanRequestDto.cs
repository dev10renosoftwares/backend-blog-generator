



namespace BlogGenerator.ServiceModels.v1;

public class CreatePlanRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Credits { get; set; }
}