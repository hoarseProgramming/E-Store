using EStore.Api.Models;
using EStore.Api.Repositories;

namespace EStore.Api.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    
    public async Task<bool> CreateAsync(Category category)
    {
        var nameIsTaken = await _categoryRepository.ExistsByCategoryNameAsync(category.CategoryName);
        
        if (nameIsTaken)
        {
            return false;
        }
        
        var created = await _categoryRepository.CreateAsync(category);

        return created;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var categoryExists = await _categoryRepository.ExistsByIdAsync(id);

        if (!categoryExists)
        {
            return false;
        }
        
        return await _categoryRepository.DeleteAsync(id);
    }
}