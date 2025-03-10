using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface IProductRepository
{
    Task<bool> CreateAsync(Product product);
    Task<Product?> GetByProductNumberAsync(int productNumber);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteByProductNumberAsync(int productNumber);
}