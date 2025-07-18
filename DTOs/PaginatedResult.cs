namespace SimpleMarket.Api.DTOs
{
    public class PaginatedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public List<T> Result { get; set; } = new List<T>();
    }
}
