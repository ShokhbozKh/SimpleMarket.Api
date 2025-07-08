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
            _context = marketDbContext ??
                throw new ArgumentNullException(nameof(marketDbContext));
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(string? name, string? desc, int? maxId, int? minId)
        {
            var query = _context.Categories.AsQueryable();

            if(!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(query => query.Name.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(desc))
            {
                query = query.Where(query => query.Description.Contains(desc));
            }
            // Filter by maxId and minId if provided
            if(minId.HasValue) // true if minId is not null
            {
                query = query.Where(query => query.Id >= minId.Value);
            }
            if(maxId.HasValue) // true if maxId is not null
            {
                query = query.Where(query => query.Id <= maxId.Value);
            }


            return await query.ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (category is null)
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
        public async Task<int> CountAsync()
        {
            return await _context.Categories.CountAsync();

        }
    }
}
