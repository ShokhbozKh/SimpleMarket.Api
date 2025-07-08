namespace SimpleMarket.Api.DTOs.Category
{
    public class CategoryFilterDto // now this Class isn't working;
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? MaxId { get; set; }
        public int? MinId { get; set; }

    }
}
