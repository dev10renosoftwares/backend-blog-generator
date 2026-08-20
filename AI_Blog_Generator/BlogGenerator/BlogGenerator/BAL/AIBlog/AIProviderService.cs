using BlogGenerator.Interfaces;

namespace BlogGenerator.BAL;

public class AIProviderService : IAIProviderService
{
    private readonly ILogger<AIProviderService> _logger;

    public AIProviderService(
        ILogger<AIProviderService> logger)
    {
        _logger = logger;
    }

    public async Task<string> GenerateBlogAsync(string prompt)
    {
        throw new NotImplementedException();
    }

    public async Task<string> RewriteBlogAsync(
        string content,
        string instructions)
    {
        throw new NotImplementedException();
    }

    public async Task<string> ExpandBlogAsync(
        string content,
        string? instructions)
    {
        throw new NotImplementedException();
    }

    public async Task<string> ShortenBlogAsync(
        string content,
        string? instructions)
    {
        throw new NotImplementedException();
    }

    public async Task<string> TranslateBlogAsync(
        string content,
        string language)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateTagsAsync(
        string content)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateSummaryAsync(
        string content)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateImageAsync(
        string prompt)
    {
        throw new NotImplementedException();
    }
}