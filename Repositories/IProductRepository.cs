using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product? product);
        Task DeleteProductAsync(int id);
        Task <bool> Exists(int id);
        Task<int> CountAsync();
    }
}
