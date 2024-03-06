// ProductDbContext.cs

using Microsoft.EntityFrameworkCore;
using ProductApp.DAL.Models;

namespace ProductApp.DAL.DataAccess
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .IsRequired();
        }
    }
}
