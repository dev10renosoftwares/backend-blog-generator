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
}