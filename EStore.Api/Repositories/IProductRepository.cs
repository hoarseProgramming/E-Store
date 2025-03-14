using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface IProductRepository
{
    void Create(Product product);
    Task<Product?> GetByProductNumberAsync(int productNumber);
    Task<IEnumerable<Product>> GetAllAsync();
    void Update(Product product);
    Task DeleteByProductNumberAsync(int productNumber);
    Task<bool> ExistsByProductNumberAsync(int productNumber);
}