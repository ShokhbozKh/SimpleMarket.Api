using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using SimpleMarket.Api.Data;
using SimpleMarket.Api.Middlewares;
using SimpleMarket.Api.Repositories;
using SimpleMarket.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// log yozish
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // asosiy loglar
    .WriteTo.File("logs/Error_log-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error) // faqat error loglar
    .CreateLogger();

builder.Host.UseSerilog();

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

// caches keshlash
builder.Services.AddResponseCaching(); // 
builder.Services.AddOutputCache(); // 
//builder.Services.AddMemoryCache();// Di ->

// Serilog konfiguratsiyasi
var app = builder.Build();
// Configure the HTTP request pipeline.

// Fake data
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
// 
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<HelloMiddleware>();
app.UseMiddleware<IpLoggingMiddleware>();

app.UseMiddleware<RequestTimingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

// keshlash
app.UseResponseCaching();
app.UseOutputCache();


app.MapControllers();

app.Run();
