using BlogGenerator.ServiceModels.v1.Category;

namespace BlogGenerator.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
}