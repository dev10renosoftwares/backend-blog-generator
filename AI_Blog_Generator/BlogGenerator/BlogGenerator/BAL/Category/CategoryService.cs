using BlogGenerator.DAL;
using BlogGenerator.Interfaces;
using BlogGenerator.ServiceModels.v1.Category;
using Microsoft.EntityFrameworkCore;

namespace BlogGenerator.BAL.Category;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        ApplicationDbContext context,
        ILogger<CategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
            .Select(x => new CategoryDto
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync();

        _logger.LogInformation(
            "Retrieved {Count} categories.",
            categories.Count);

        return categories;
    }
}