using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class ProductRepository(EStoreContext eStoreContext) : IProductRepository
{
    
    public async Task<bool> CreateAsync(Product product)
    {
        eStoreContext.Products.Add(product);
        
        await eStoreContext.Database.OpenConnectionAsync();
        try
        {
            await eStoreContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Products ON");
            await eStoreContext.SaveChangesAsync();
            await eStoreContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Products OFF");
        }
        finally
        {
            await eStoreContext.Database.CloseConnectionAsync();
        }
        
        return true;
    }

    public async Task<Product?> GetByProductNumberAsync(int productNumber)
    {
        var product = eStoreContext.Products.SingleOrDefault(p => p.ProductNumber == productNumber);

        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await eStoreContext.Products.ToListAsync();
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        eStoreContext.Update(product);
        
        var result = await eStoreContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteByProductNumberAsync(int productNumber)
    {
        var product = await eStoreContext.Products.SingleOrDefaultAsync(p => p.ProductNumber == productNumber);

        eStoreContext.Remove(product);

        var result = await eStoreContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> ExistsByProductNumberAsync(int productNumber)
    {
        var productExists = await eStoreContext.Products.AnyAsync(p => p.ProductNumber == productNumber);

        return productExists;
    }
}