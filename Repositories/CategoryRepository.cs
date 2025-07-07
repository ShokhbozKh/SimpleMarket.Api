using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.Data;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly MarketDbContext _context;

        public CategoryRepository(MarketDbContext marketDbContext)
        {
            _context = marketDbContext?? 
                throw new ArgumentNullException(nameof(marketDbContext));  
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var result = await _context.Categories.ToListAsync();
            return result;
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            
            return result;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if(category is null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;    
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Exists(int id)
        {
            return await _context.Categories.AnyAsync(x => x.Id == id);
        }

    }
}
