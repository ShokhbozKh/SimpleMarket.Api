using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.Data;
using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.DTOs.Product;
using SimpleMarket.Api.Models;
using SimpleMarket.Api.Services;

namespace SimpleMarket.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
           
        }
        // GET: api/products
        [HttpGet]
        [OutputCache(Duration = 10)] // 1 sekund
        public async Task<ActionResult<PaginatedResult<ReadProductDto>>> GetAllAsync([FromQuery] ProductFilterDto filterDto)
        {
            var products = await _productService.GetAllProductsAsync(filterDto); //hfj
            if (products == null)
            {
                return NotFound("No products found.");
            }

            return Ok(products); // Return the list of products
        }
        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProductDto>> GetByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            if (product.CategoryId < 0)
            {
                return NotFound("Category not found for this product.");
            }
            return Ok(product); // Return the product
        }
        [HttpGet("Count")]
        public async Task<ActionResult<int>> GetCountProductsAsync()
        {
            var count = await _productService.CountProductsAsync();
            if (count < 0)
            {
                return NotFound("No products found.");
            }
            return Ok(count); // Return the count of products
        }
        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ReadProductDto>> Post([FromBody] CreateProductDto? product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }
            

            if (string.IsNullOrWhiteSpace(product.Name) || product.CategoryId < 0)
            {
                return BadRequest("Product name cannot be empty and CategoryId must be provided.");
            }

            var createdProduct = await _productService.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdProduct.Id }, createdProduct);
        }
        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductDto? product)
        {
            if (id<0)
            {
                return BadRequest();
            }
            await _productService.UpdateProductAsync(id, product);

            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            if (id < 0)
            {
                return BadRequest("Invalid product ID.");
            }
            await _productService.DeleteProductAsync(id);
            
            
            return NoContent();
        }
    }
}
