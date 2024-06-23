using MVCProject_API.DTOs.CategoryDto;
using MVCProject_API.Models;

namespace MVCProject_API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesWithCourses();
        Task Create(CategoryCreateDto category);
        Task Edit(Category category, CategoryEditDto request);
        Task Delete(Category category);
        Task<bool> ExistCategory(string name);
        Task<Category> GetById(int id);
        Task<List<CategoryDto>> GetAll();
    }
}
