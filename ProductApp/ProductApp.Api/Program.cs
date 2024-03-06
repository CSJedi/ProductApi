using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProductApp.Core.Interfaces;
using ProductApp.Core.Services;
using ProductApp.DAL.DataAccess;
using ProductApp.DAL.Interfaces;
using ProductApp.DAL.Repositories;
using ProductApp.Infrastructure.Logging;
using ProductApp.Infrastructure.Logging.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite("Data Source=..\\..\\products.sqlite"), ServiceLifetime.Scoped);
builder.Services.AddScoped<ILoggingService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var logFilePath = $"..\\..\\{DateTime.Now}_LogFile.log";
    return new LoggingService(logFilePath);
});
// Register other services and repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();

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
