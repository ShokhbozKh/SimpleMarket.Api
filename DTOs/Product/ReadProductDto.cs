namespace SimpleMarket.Api.DTOs.Product
{
    public class ReadProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; } // Nullable to allow products without a category

        // Additional properties can be added as needed
        // For example, you might want to include a property for the product's image URL or stock quantity
    }
}
