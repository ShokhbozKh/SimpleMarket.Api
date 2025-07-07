namespace SimpleMarket.Api.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // Additional properties can be added as needed
        // For example, you might want to include a property for the category's image URL or parent category ID
    }
}
