using EStore.Api.Models;
using EStore.Api.Repositories;
using EStore.Api.UnitOfWork;

namespace EStore.Api.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<bool> CreateAsync(Category category)
    {
        var nameIsTaken = await _unitOfWork.CategoryRepository.ExistsByCategoryNameAsync(category.CategoryName);
        
        if (nameIsTaken)
        {
            return false;
        }
        
        _unitOfWork.CategoryRepository.Create(category);

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CategoryRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _unitOfWork.CategoryRepository.GetAllAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var categoryExists = await _unitOfWork.CategoryRepository.ExistsByIdAsync(id);

        if (!categoryExists)
        {
            return false;
        }
        
        await _unitOfWork.CategoryRepository.DeleteAsync(id);
        
        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }
}