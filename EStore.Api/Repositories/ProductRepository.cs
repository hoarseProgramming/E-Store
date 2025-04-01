using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class ProductRepository(EStoreContext eStoreContext) : IProductRepository
{

    public void Create(Product product)
    {
        eStoreContext.Products.Add(product);
    }

    public async Task<Product?> GetByProductNumberAsync(int productNumber)
    {
        var product = await eStoreContext.Products
            .Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.ProductNumber == productNumber);

        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await eStoreContext.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public void Update(Product product)
    {
        eStoreContext.Update(product);
    }

    public async Task DeleteByProductNumberAsync(int productNumber)
    {
        var product = await eStoreContext.Products.SingleOrDefaultAsync(p => p.ProductNumber == productNumber);

        eStoreContext.Remove(product);
    }

    public async Task<bool> ExistsByProductNumberAsync(int productNumber)
    {
        var productExists = await eStoreContext.Products.AnyAsync(p => p.ProductNumber == productNumber);

        return productExists;
    }
}