using EStore.Api.Models;
using EStore.Api.Repositories;
using EStore.Api.UnitOfWork;

namespace EStore.Api.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<bool> CreateAsync(Product product)
    {
        var productNumberIsInDatabase = await _unitOfWork.ProductRepository.ExistsByProductNumberAsync(product.ProductNumber);

        if (productNumberIsInDatabase)
        {
            return false;
        }

        _unitOfWork.ProductRepository.Create(product);

        var result = await _unitOfWork.SaveChangesWithModifiedIdentityAsync();

        return result > 0;
    }

    public Task<Product?> GetByProductNumberAsync(int productNumber)
    {
        return _unitOfWork.ProductRepository.GetByProductNumberAsync(productNumber);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return _unitOfWork.ProductRepository.GetAllAsync();
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var productExists = await _unitOfWork.ProductRepository.ExistsByProductNumberAsync(product.ProductNumber);

        if (!productExists)
        {
            return false;
        }

        _unitOfWork.ProductRepository.Update(product);

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteByProductNumberAsync(int productNumber)
    {
        var productExists = await _unitOfWork.ProductRepository.ExistsByProductNumberAsync(productNumber);
        
        if (!productExists)
        {
            return false;
        }

        await _unitOfWork.ProductRepository.DeleteByProductNumberAsync(productNumber);

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }
}