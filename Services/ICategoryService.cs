using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.DTOs.Category;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Services
{
    public interface ICategoryService
    {
        Task<PaginatedResult<ReadCategoryDto>> GetAllCategoriesAsync(CategoryFilterDto categoryFilterDto);
        Task<ReadCategoryDto> GetCategoryByIdAsync(int id);
        Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
        Task<int> CountCategoriesAsync();

    }
}
