namespace SimpleMarket.Api.DTOs.Product
{
    public class ProductFilterDto
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public int? MaxId { get; set; }
        public int? MinId { get; set; }
        public int? CategoryId { get; set; }

    }
}
