namespace SimpleMarket.Api.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } 
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; } 

    }
}
