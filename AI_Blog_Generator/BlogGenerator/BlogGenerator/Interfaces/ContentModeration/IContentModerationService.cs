using BlogGenerator.ServiceModels.v1.ContentModeration;

namespace BlogGenerator.Interfaces.ContentModeration;

public interface IContentModerationService
{
    Task<AIContentScanResult> ScanBlogContentAsync(
        string title,
        string content);
}