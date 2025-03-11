using EStore.Api.Models;

namespace EStore.Api.Services;

public interface IProductService
{
    Task<bool> CreateAsync(Product product);
    Task<Product?> GetByProductNumberAsync(int productNumber);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteByProductNumberAsync(int productNumber);
}