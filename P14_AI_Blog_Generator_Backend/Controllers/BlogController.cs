using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P14_AI_Blog_Generator_Backend.ApiResponse;
using P14_AI_Blog_Generator_Backend.DTOs.Blogs;
using P14_AI_Blog_Generator_Backend.Interfaces;

namespace P14_AI_Blog_Generator_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/blogs")]
public class BlogsController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogsController(IBlogService blogService)
    {
        _blogService = blogService;
    }
    private int userId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        var result = await _blogService.GetBlogsAsync(userId);

        return Ok(new ApiResponse<List<BlogListDto>>
        {
            Success = true,
            Message = "Blogs retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{blogId}")]
    public async Task<IActionResult> GetBlog(int blogId)
    {
        var result = await _blogService.GetBlogByIdAsync(userId, blogId);

        return Ok(new ApiResponse<BlogDetailsDto>
        {
            Success = true,
            Message = "Blog retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{blogId}/versions")]
    public async Task<IActionResult> GetVersions(int blogId)
    {
        var result = await _blogService.GetBlogVersionsAsync(userId, blogId);

        return Ok(new ApiResponse<List<BlogVersionDto>>
        {
            Success = true,
            Message = "Blog versions retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{blogId}/images")]
    public async Task<IActionResult> GetImages(int blogId)
    {
        var result = await _blogService.GetBlogImagesAsync(userId, blogId);

        return Ok(new ApiResponse<List<BlogImageDto>>
        {
            Success = true,
            Message = "Blog images retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{blogId}/download-pdf")]
    public async Task<IActionResult> DownloadPdf(int blogId)
    {
        var pdf = await _blogService.DownloadPdfAsync(userId, blogId);

        return File(
            pdf,
            "application/pdf",
            $"Blog_{blogId}.pdf");
    }

    [HttpDelete("{blogId}")]
    public async Task<IActionResult> DeleteBlog(int blogId)
    {
        await _blogService.DeleteBlogAsync(userId, blogId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Blog deleted successfully."
        });
    }

    [HttpDelete("images/{imageId}")]
    public async Task<IActionResult> DeleteImage(int imageId)
    {
        await _blogService.DeleteImageAsync(userId, imageId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Image deleted successfully."
        });
    }
}