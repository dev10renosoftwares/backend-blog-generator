using BlogGenerator.DAL;
using BlogGenerator.Foundation.Exceptions;
using BlogGenerator.Interfaces.Blog;
using BlogGenerator.ServiceModels.v1.Blog;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

namespace BlogGenerator.BAL.Blog;

public class BlogService : IBlogService
{
    private readonly ApplicationDbContext _context;

    private readonly ILogger<BlogService> _logger;

    public BlogService(
        ApplicationDbContext context,
        ILogger<BlogService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<BlogListDto>> GetBlogsAsync(int userId)
    {
        var blogs = await _context.Blogs
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new BlogListDto
            {
                BlogId = x.BlogId,
                Title = x.Title,
                Category = x.Category,
                WordCount = x.WordCount,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt ?? x.CreatedAt
            })
            .ToListAsync();

        _logger.LogInformation(
            "Retrieved {Count} blogs for User {UserId}.",
            blogs.Count,
            userId);

        return blogs;
    }

    public async Task<BlogDetailsDto> GetBlogByIdAsync(
    int userId,
    int blogId)
    {
        var blog = await _context.Blogs
            .FirstOrDefaultAsync(x =>
                x.BlogId == blogId &&
                x.UserId == userId);

        if (blog == null)
        {
            _logger.LogWarning(
                "Blog {BlogId} not found for User {UserId}.",
                blogId,
                userId);

            throw new NotFoundException("Blog not found.");
        }

        _logger.LogInformation(
            "Retrieved Blog {BlogId} for User {UserId}.",
            blogId,
            userId);

        return new BlogDetailsDto
        {
            BlogId = blog.BlogId,
            Title = blog.Title,
            Prompt = blog.Prompt,
            Content = blog.Content,
            Tone = blog.Tone,
            Audience = blog.Audience,
            Category = blog.Category,
            WordCount = blog.WordCount,
            CreditsUsed = blog.CreditsUsed,
            CreatedAt = blog.CreatedAt,
            UpdatedAt = blog.UpdatedAt ?? blog.CreatedAt
        };
    }

    public async Task<List<BlogVersionDto>> GetBlogVersionsAsync(
     int userId,
     int blogId)
    {
        var blogExists = await _context.Blogs
            .AnyAsync(x =>
                x.BlogId == blogId &&
                x.UserId == userId);

        if (!blogExists)
        {
            throw new NotFoundException("Blog not found.");
        }

        var versions = await _context.BlogVersions
            .Where(x => x.BlogId == blogId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new BlogVersionDto
            {
                VersionId = x.VersionId,
                Title = x.Title,
                VersionType = x.VersionType.ToString(),
                Content = x.Content,
                WordCount = x.WordCount,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        _logger.LogInformation(
            "Retrieved {Count} versions for Blog {BlogId}.",
            versions.Count,
            blogId);

        return versions;
    }

    public async Task<List<BlogImageDto>> GetBlogImagesAsync(
     int userId,
     int blogId)
    {
        var blogExists = await _context.Blogs
            .AnyAsync(x =>
                x.BlogId == blogId &&
                x.UserId == userId);

        if (!blogExists)
        {
            throw new NotFoundException("Blog not found.");
        }

        var images = await _context.BlogImages
            .Where(x => x.BlogId == blogId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new BlogImageDto
            {
                ImageId = x.ImageId,
                Prompt = x.Prompt,
                ImageUrl = x.ImageUrl,
                CreditsUsed = x.CreditsUsed,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        _logger.LogInformation(
            "Retrieved {Count} images for Blog {BlogId}.",
            images.Count,
            blogId);

        return images;
    }

    public async Task<byte[]> DownloadPdfAsync(
     int userId,
     int blogId)
    {
        var blog = await _context.Blogs
            .FirstOrDefaultAsync(x =>
                x.BlogId == blogId &&
                x.UserId == userId);

        if (blog == null)
        {
            _logger.LogWarning(
                "Blog {BlogId} not found for User {UserId}.",
                blogId,
                userId);

            throw new NotFoundException("Blog not found.");
        }

        _logger.LogInformation(
            "Generating PDF for Blog {BlogId}.",
            blogId);

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Text(blog.Title)
                    .FontSize(24)
                    .Bold();

                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Category: {blog.Category}");
                        column.Item().Text($"Audience: {blog.Audience}");
                        column.Item().Text($"Tone: {blog.Tone}");
                        column.Item().Text($"Word Count: {blog.WordCount}");

                        column.Item().PaddingTop(20);

                        column.Item().Text(blog.Content);
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Generated by AI Blog Generator");
                    });
            });
        });

        return pdf.GeneratePdf();
    }

    public async Task DeleteBlogAsync(
    int userId,
    int blogId)
    {
        var blog = await _context.Blogs
            .Include(x => x.BlogVersions)
            .Include(x => x.BlogImages)
            .FirstOrDefaultAsync(x =>
                x.BlogId == blogId &&
                x.UserId == userId);

        if (blog == null)
        {
            _logger.LogWarning(
                "Delete failed. Blog {BlogId} not found for User {UserId}.",
                blogId,
                userId);

            throw new NotFoundException("Blog not found.");
        }

        _context.BlogVersions.RemoveRange(blog.BlogVersions);

        _context.BlogImages.RemoveRange(blog.BlogImages);

        _context.Blogs.Remove(blog);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Blog {BlogId} deleted successfully.",
            blogId);
    }

    public async Task DeleteImageAsync(
     int userId,
     int imageId)
    {
        var image = await _context.BlogImages
            .Include(x => x.Blog)
            .FirstOrDefaultAsync(x => x.ImageId == imageId);

        if (image == null)
        {
            _logger.LogWarning(
                "Image {ImageId} not found.",
                imageId);

            throw new NotFoundException("Image not found.");
        }

        if (image.Blog.UserId != userId)
        {
            _logger.LogWarning(
                "User {UserId} attempted to delete another user's image {ImageId}.",
                userId,
                imageId);

            throw new ForbiddenException("You are not authorized to delete this image.");
        }

        _context.BlogImages.Remove(image);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Image {ImageId} deleted successfully.",
            imageId);
    }
}