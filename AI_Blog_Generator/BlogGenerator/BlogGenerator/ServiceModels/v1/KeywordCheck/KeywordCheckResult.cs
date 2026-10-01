namespace BlogGenerator.ServiceModels.v1.KeywordRestriction;

public class KeywordCheckResult
{
    public bool IsAllowed { get; set; }

    public string Message { get; set; } = string.Empty;

    public List<string> Violations { get; set; } = new();
}