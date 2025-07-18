using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.Data;
using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.DTOs.Category;
using SimpleMarket.Api.Models;
using SimpleMarket.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleMarket.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryService category, ILogger<CategoriesController> logger)
        {
            _categoryService = category ?? throw new ArgumentNullException(nameof(category));
            _logger = logger;
        }
        // GET: api/categories
        [HttpGet]
        [ResponseCache(Duration = 10)] // cache
        public async Task <ActionResult<PaginatedResult<ReadCategoryDto>>> Get([FromQuery]CategoryFilterDto categoryFilter)
        {
            _logger.LogInformation("Categoriyalar olinmoqda .....");

            var result = await _categoryService.GetAllCategoriesAsync(categoryFilter);

            if(result == null)
            {
                return NotFound("No categories found.");
            }
            _logger.LogInformation($"{result.TotalCount} ta malumot topildi");

            // Pagination
           
            return Ok(result);
        }

        // GET api/categores/5
        [HttpGet("{id}")]
        public async Task <ActionResult<ReadCategoryDto>> GetById([FromRoute]int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(result);
        }
        [HttpGet("Count")]
        public async Task<ActionResult<IEnumerable<ReadCategoryDto>>> GetCountCategories()
        {
            var result = await _categoryService.CountCategoriesAsync();
            if (result < 0)
            {
                return NotFound("No categories found.");
            }
            return Ok(result);
        }

        // POST api/categories
        [HttpPost]
        public async Task<ActionResult<ReadCategoryDto>> Post([FromBody] CreateCategoryDto newCategory)
        { 
           var result = await _categoryService.CreateCategoryAsync(newCategory);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryDto updateCategory)
        {
            if (updateCategory == null || id<0)
            {
                return BadRequest("Update data cannot be null.");
            }
            var exists = await _categoryService.CategoryExistsAsync(id);
            if (!exists)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            await _categoryService.UpdateCategoryAsync(id, updateCategory);
            return NoContent();
            
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _categoryService.CategoryExistsAsync(id))
            {
                return NotFound($"Category with ID {id} not found.");
            }
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
