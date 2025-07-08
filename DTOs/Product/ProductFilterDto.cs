namespace SimpleMarket.Api.DTOs.Product
{
    public class ProductFilterDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public int? MaxId { get; set; }
        public int? MinId { get; set; }
        public int? CategoryId { get; set; }

    }
}
