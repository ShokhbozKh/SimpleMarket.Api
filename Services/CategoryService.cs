using SimpleMarket.Api.DTOs.Category;
using SimpleMarket.Api.Models;
using SimpleMarket.Api.Repositories;

namespace SimpleMarket.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository category )
        {
            _categoryRepository = category ?? throw new ArgumentNullException(nameof(category));
        }
        public async Task<IEnumerable<ReadCategoryDto>> GetAllCategoriesAsync()
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();

            if (allCategories == null)
            {
                return null;
            }
            var result = allCategories.Select(c=> new ReadCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return result;
        }

        public async Task<ReadCategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            var result = new ReadCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return result;
        }

        public async Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                throw new ArgumentException("Category name cannot be empty.", nameof(categoryDto.Name));
            }

            var newCategory = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description

            };
            var createdCategory = await _categoryRepository.CreateCategoryAsync(newCategory);

            var result = new ReadCategoryDto
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                Description = createdCategory.Description
            };
            return result;

        }

       
        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
        {
            var result = await _categoryRepository.GetByIdAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto), "Update data cannot be null.");
            }

            var updatedCategory = new Category
            {   Id = id,
                Name = categoryDto.Name?? result.Name,
                Description = categoryDto.Description?? result.Description
            };
            await _categoryRepository.UpdateCategoryAsync(updatedCategory);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var deleteCategory = await _categoryRepository.GetByIdAsync(id);
            if (deleteCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            await _categoryRepository.DeleteCategoryAsync(deleteCategory);
        }
        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepository.Exists(id);
        }
    }
}
