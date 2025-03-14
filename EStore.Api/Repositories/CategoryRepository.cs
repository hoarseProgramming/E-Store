using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class CategoryRepository(EStoreContext eStoreContext) : ICategoryRepository
{
    private readonly EStoreContext _eStoreContext = eStoreContext;
    
    public void Create(Category category)
    {
        _eStoreContext.Categories.Add(category);
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

    //TODO: Remove this method
    // public async Task<bool> DeleteAsync(Guid id)
    // {
    //     var category = await _eStoreContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
    //
    //     _eStoreContext.Remove(category);
    //
    //     var result = await _eStoreContext.SaveChangesAsync();
    //
    //     return result > 0;
    // }
    
    public async Task DeleteAsync(Guid id)
    {
        var category = await _eStoreContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

        _eStoreContext.Remove(category);
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