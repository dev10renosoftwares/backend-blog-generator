using BlogGenerator.ServiceModels.v1.Blog;

namespace BlogGenerator.Interfaces.Blog;

public interface IBlogService
{
    Task<List<BlogListDto>> GetBlogsAsync(
        int userId);

    Task<BlogDetailsDto> GetBlogByIdAsync(
        int userId,
        int blogId);

    Task<List<BlogVersionDto>> GetBlogVersionsAsync(
        int userId,
        int blogId);

    Task<List<BlogImageDto>> GetBlogImagesAsync(
        int userId,
        int blogId);

    Task<byte[]> DownloadPdfAsync(
        int userId,
        int blogId);

    Task DeleteBlogAsync(
        int userId,
        int blogId);

    Task DeleteImageAsync(
        int userId,
        int imageId);

    Task<BlogResponseDto> PublishBlogAsync(
        int blogId,
        int userId);

    Task<BlogResponseDto> UnpublishBlogAsync(
        int blogId,
        int userId);

    Task<BlogResponseDto> UpdateBlogAsync(
        int blogId,
        int userId,
        UpdateBlogRequestDto request);

    Task<List<BlogResponseDto>> GetDraftBlogsAsync(
        int userId);

    Task<List<BlogResponseDto>> GetPublishedBlogsAsync(
        int userId);
}