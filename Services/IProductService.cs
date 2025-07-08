using SimpleMarket.Api.DTOs.Product;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Services
{
    public interface IProductService
    {
        Task <IEnumerable<ReadProductDto>> GetAllProductsAsync(ProductFilterDto filterDto);
        Task <ReadProductDto> GetProductByIdAsync(int id);
        Task<ReadProductDto> CreateProductAsync(CreateProductDto? product);
        Task UpdateProductAsync(int id, UpdateProductDto? product);
        Task DeleteProductAsync(int id);
        Task <int> CountProductsAsync();


    }
}
