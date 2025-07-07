using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.Models;

namespace SimpleMarket.Api.Data
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // Set precision for Price
            modelBuilder.Entity<Product>()
                .Property(i=>i.Id)
                .ValueGeneratedOnAdd(); // Ensure Id is generated on add


        }
    }

    
}
