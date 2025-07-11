﻿using SimpleMarket.Api.DTOs.Category;

namespace SimpleMarket.Api.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<ReadCategoryDto>> GetAllCategoriesAsync(string? search, string? description, int? maxId, int? minId);
        Task<ReadCategoryDto> GetCategoryByIdAsync(int id);
        Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
        Task<int> CountCategoriesAsync();

    }
}
