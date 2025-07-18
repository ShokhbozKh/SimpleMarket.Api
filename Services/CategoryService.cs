using SimpleMarket.Api.DTOs;
using SimpleMarket.Api.DTOs.Category;
using SimpleMarket.Api.Exceptions;
using SimpleMarket.Api.Models;
using SimpleMarket.Api.Repositories;

namespace SimpleMarket.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository category )
        {
            _categoryRepository = category ?? throw new CustomException($" Server isnt working:{nameof(category).ToString()}", 500);
        }
        public async Task<PaginatedResult<ReadCategoryDto>> GetAllCategoriesAsync(CategoryFilterDto categoryFilterDto)
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync(categoryFilterDto);

            if (allCategories == null)
            {
                throw new CustomException("CAtegory is null", 404);
            }
           

            return allCategories;
        }

        public async Task<ReadCategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new CustomException($"Id:{id} aniqlanmadi", 404);
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
                throw new CustomException($"Category name cannot be empty, { nameof(categoryDto.Name) }", 404);
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
                throw new CustomException($"Category with ID {id} not found.", 404);
            }
            if (categoryDto == null)
            {
                throw new CustomException($"{nameof(categoryDto)}, Update data cannot be null.", 404);
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
                throw new CustomException($"Category with ID {id} not found.", 404);
            }
            await _categoryRepository.DeleteCategoryAsync(deleteCategory);
        }
        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepository.Exists(id);
        }
        public async Task<int> CountCategoriesAsync()
        {
            var allCategories = await _categoryRepository.CountAsync();
            return allCategories;
        }
    }
}
