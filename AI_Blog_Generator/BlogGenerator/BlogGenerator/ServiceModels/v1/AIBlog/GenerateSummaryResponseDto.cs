namespace BlogGenerator.ServiceModels.v1.AIBlog;

public class GenerateSummaryResponseDto
{
    public int BlogId { get; set; }

    public string Summary { get; set; } = string.Empty;
}