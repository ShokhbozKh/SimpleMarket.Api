using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<bool> Exists(int id);
        

    }
}
