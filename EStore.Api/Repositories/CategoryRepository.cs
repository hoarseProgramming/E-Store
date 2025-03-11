using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class CategoryRepository(EStoreContext eStoreContext) : ICategoryRepository
{
    private readonly EStoreContext _eStoreContext = eStoreContext;
    
    public async Task<bool> CreateAsync(Category category)
    {
        _eStoreContext.Categories.Add(category);

        var result = await _eStoreContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var category = await _eStoreContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _eStoreContext.Categories.ToListAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _eStoreContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

        _eStoreContext.Remove(category);

        var result = await _eStoreContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        var categoryExists = await _eStoreContext.Categories.AnyAsync(c => c.Id == id);

        return categoryExists;
    }

    public async Task<bool> ExistsByCategoryNameAsync(string categoryName)
    {
        var categoryExists = await _eStoreContext.Categories.AnyAsync(c => c.CategoryName.ToUpper() == categoryName.ToUpper());

        return categoryExists;
    }
}