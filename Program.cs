using Microsoft.EntityFrameworkCore;
using SimpleMarket.Api.Data;
using SimpleMarket.Api.Repositories;
using SimpleMarket.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register the DbContext with dependency injection
builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the repository interface and implementation
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Register the service interface and implementation
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();// bu -> service da ishlatish uchun kerak
// Register the service interface and implementation for categories
builder.Services.AddScoped<ICategoryService, CategoryService>(); // bu -> controller da ishlatish uchun kerak


var app = builder.Build();

// Register the fake data seeder
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MarketDbContext>();

    FakeDataSeeder.SeedCategoryData(context, 50); // Baza to‘ldiriladi
    FakeDataSeeder.SeedProductData(context, 50); // Baza to‘ldiriladi
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
