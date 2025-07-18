namespace SimpleMarket.Api.DTOs.Category
{
    public class CategoryFilterDto 
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? MaxId { get; set; }
        public int? MinId { get; set; }

    }
}
