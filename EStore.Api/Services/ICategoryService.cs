using EStore.Api.Models;

namespace EStore.Api.Services;

public interface ICategoryService
{
    public Task<bool> CreateAsync(Category category);
    public Task<Category?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task<bool> DeleteAsync(Guid id);
}