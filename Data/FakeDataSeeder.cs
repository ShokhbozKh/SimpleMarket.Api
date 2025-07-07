using Bogus;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Data
{
    public static class FakeDataSeeder
    {
        public static void SeedCategoryData(MarketDbContext context, int num)
        {
            if (context.Categories.Any())
            {
                return; // Data already seeded
            }

            // Seed Categories
            Faker<Category> faker = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).FirstOrDefault())
                .RuleFor(c=>c.Description, f=>f.Commerce.ProductDescription()); 

            var categories = faker.Generate(num); // Generate 'num' categories
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        public static void SeedProductData(MarketDbContext context, int num)
        {
            if (context.Products.Any())
            {
                return;
            }
            var categoryIds = context.Categories.Select(c => c.Id).ToList();
            // Seed Products
            Faker <Product> faker  = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Finance.Amount(1, 1000, 2))
                .RuleFor(p => p.CategoryId, f=>f.PickRandom(categoryIds));
            var products = faker.Generate(num); // Generate 'num' products
            context.Products.AddRange(products);
            context.SaveChanges();
        }

    }
}
