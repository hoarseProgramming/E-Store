using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Database;

public class EStoreContext(DbContextOptions<EStoreContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(product => product.ProductNumber);
        
        modelBuilder.Entity<Product>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(e => e.CategoryId);

    }
}