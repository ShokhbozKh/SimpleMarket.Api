using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.DTOs.Product;
using SimpleMarket.Api.Models;
using SimpleMarket.Api.Repositories;

namespace SimpleMarket.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<PaginatedResult<ReadProductDto>> GetAllProductsAsync(ProductFilterDto filterDto)
        {
            var result = await _productRepository.GetAllProductsAsync(filterDto);

            if (result == null)
            {
                throw new ArgumentNullException("No products found.", nameof(result));
            }

            

            return result;
        }

        public async Task<ReadProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return null; // or throw an exception if preferred

            }
            var readProductDto = new ReadProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
            return readProductDto;
        }
        public async Task<ReadProductDto> CreateProductAsync(CreateProductDto? product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            var createdProduct = await _productRepository.CreateProductAsync(newProduct);
            if (createdProduct == null)
            {
                throw new InvalidOperationException("Failed to create product.");
            }
            var readProductDto = new ReadProductDto
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Description = createdProduct.Description,
                Price = createdProduct.Price,
                CategoryId = createdProduct.CategoryId

            };
            return readProductDto;
        }


        public async Task UpdateProductAsync(int id, UpdateProductDto? product)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            existingProduct.Name = product?.Name ?? existingProduct.Name;
            existingProduct.Description = product?.Description ?? existingProduct.Description;
            existingProduct.Price = product?.Price ?? existingProduct.Price;
            existingProduct.CategoryId = product?.CategoryId ?? existingProduct.CategoryId;

            await _productRepository.UpdateProductAsync(existingProduct);

        }

        public async Task DeleteProductAsync(int id)
        {
            if (!await _productRepository.Exists(id))
            {
                return;
            }
            await _productRepository.DeleteProductAsync(id);

        }
        public async Task<int> CountProductsAsync()
        {
            var products = await _productRepository.CountAsync();
            return products;


        }
    }
}
