using EStore.Api.Models;
using EStore.Api.Repositories;

namespace EStore.Api.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<bool> CreateAsync(Product product)
    {
        var productNumberIsInDatabase = await _productRepository.ExistsByProductNumberAsync(product.ProductNumber);

        if (productNumberIsInDatabase)
        {
            return false;
        }

        return await _productRepository.CreateAsync(product);
    }

    public Task<Product?> GetByProductNumberAsync(int productNumber)
    {
        return _productRepository.GetByProductNumberAsync(productNumber);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return _productRepository.GetAllAsync();
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var productExists = await _productRepository.ExistsByProductNumberAsync(product.ProductNumber);

        if (!productExists)
        {
            return false;
        }

        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> DeleteByProductNumberAsync(int productNumber)
    {
        var productExists = await _productRepository.ExistsByProductNumberAsync(productNumber);
        
        if (!productExists)
        {
            return false;
        }

        return await _productRepository.DeleteByProductNumberAsync(productNumber);
    }
}