﻿namespace SimpleMarket.Api.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; } // Assuming CategoryId is an integer
    }
}
