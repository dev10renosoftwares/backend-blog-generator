

namespace BlogGenerator.Interfaces;

public interface IAIProviderService
{
    Task<string> GenerateBlogAsync(string prompt);

    Task<string> RewriteBlogAsync(string content, string instructions);

    Task<string> ExpandBlogAsync(string content, string? instructions);

    Task<string> ShortenBlogAsync(string content, string? instructions);

    Task<string> TranslateBlogAsync(string content, string language);

    Task<string> GenerateTagsAsync(string content);

    Task<string> GenerateSummaryAsync(string content);

    Task<string> GenerateImageAsync(string prompt);
}