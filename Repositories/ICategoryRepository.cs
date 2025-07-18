using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.DTOs.Category;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<PaginatedResult<ReadCategoryDto>> GetAllCategoriesAsync(CategoryFilterDto categoryFilterDto);
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<bool> Exists(int id);
        Task<int> CountAsync();


    }
}
