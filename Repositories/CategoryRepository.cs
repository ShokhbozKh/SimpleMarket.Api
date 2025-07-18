using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.Data;
using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.DTOs.Category;
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

        public async Task<PaginatedResult<ReadCategoryDto>> GetAllCategoriesAsync(CategoryFilterDto categoryFilterDto)
        {
            var query = _context.Categories
                .SelectMany(p=>p.Products)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(categoryFilterDto.Name))
            {
                query = query.Where(query => query.Name.Contains(categoryFilterDto.Name));
            }
            if (!string.IsNullOrWhiteSpace(categoryFilterDto.Description))
            {
                query = query.Where(query => query.Description.Contains(categoryFilterDto.Description));
            }
            // Filter by maxId and minId if provided
            if(categoryFilterDto.MinId.HasValue) // true if minId is not null
            {
                query = query.Where(query => query.Id >= categoryFilterDto.MinId);
            }
            if(categoryFilterDto.MinId.HasValue) // true if maxId is not null
            {
                query = query.Where(query => query.Id <= categoryFilterDto.MaxId);
            }
            // [1,2,3,4,5,6,7,8,9,0]
            // skip(2) => 7,8,9
            // take(3)
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / categoryFilterDto.PageSize);

            query = query.Skip((categoryFilterDto.PageNumber-1)*categoryFilterDto.PageSize)
                .Take(categoryFilterDto.PageSize);


            var resultPages = new PaginatedResult<ReadCategoryDto>
            {
                PageNumber = categoryFilterDto.PageNumber,
                PageSize = categoryFilterDto.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Result = await query.Select(category => new ReadCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                }).ToListAsync()

            }
            ;
            return Task.FromResult( resultPages).Result;
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
