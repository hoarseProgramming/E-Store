using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface ICategoryRepository
{
    public Task<bool> CreateAsync(Category category);
    public Task<Category?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task<bool> DeleteAsync(Guid id);
    public Task<bool> ExistsByIdAsync(Guid id);
    public Task<bool> ExistsByCategoryNameAsync(string categoryName);

}