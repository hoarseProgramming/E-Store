using EStore.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Database;

public class EStoreContext : IdentityDbContext<AuthUser>
{
    public EStoreContext(DbContextOptions<EStoreContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuthUser>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(c => c.CustomerId)
            .IsRequired(false);

        modelBuilder.Entity<Product>()
            .HasKey(product => product.ProductNumber);

        modelBuilder.Entity<Product>()
            .HasOne(c => c.Category)
            .WithMany()
            .HasForeignKey(c => c.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);


        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne();

        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.ProductNumber, op.OrderId });

        modelBuilder.Entity<OrderProduct>()
            .HasOne<Order>()
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OrderProduct>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(e => e.ProductNumber)
            .OnDelete(DeleteBehavior.NoAction);



    }
}