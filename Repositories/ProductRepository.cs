using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.Data;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketDbContext _context;
        public ProductRepository(MarketDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category) // Include related Category data
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category) // Include related Category data
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }
            return product;
        }

        // xatolik shu yerda bo'lishi mumkin, agar CreateProductAsync metodida EntityState.Detached ishlatilsa,
        // bu metodni chaqirganda Product entity'si tracking qilinmaydi va uni yangilash yoki o'chirish qiyinlashadi.
        public async Task<Product> CreateProductAsync(Product product)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);
            if (!categoryExists)
            {
                throw new InvalidOperationException($"Category with ID {product.CategoryId} does not exist.");
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();


            return product;
        }
        public async Task UpdateProductAsync(Product? product)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteProductAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();

        }
        public async Task<bool> Exists(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }
    }
}
