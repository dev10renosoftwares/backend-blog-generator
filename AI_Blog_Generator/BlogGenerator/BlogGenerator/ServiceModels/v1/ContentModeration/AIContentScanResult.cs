namespace BlogGenerator.ServiceModels.v1.ContentModeration;

public class AIContentScanResult
{
    public bool IsSafe { get; set; }

    public string? Reason { get; set; }

    public List<string> Violations { get; set; } = new();
}