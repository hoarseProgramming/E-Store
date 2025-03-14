using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface ICategoryRepository
{
    public void Create(Category category);
    public Task<Category?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task DeleteAsync(Guid id);
    public Task<bool> ExistsByIdAsync(Guid id);
    public Task<bool> ExistsByCategoryNameAsync(string categoryName);

}