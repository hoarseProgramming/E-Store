using EStore.Api.Models;
using EStore.Api.Repositories;
using EStore.Api.UnitOfWork;

namespace EStore.Api.Services;

public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> CreateAsync(Customer customer)
    {
        _unitOfWork.CustomerRepository.Create(customer);

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return _unitOfWork.CustomerRepository.GetByIdAsync(id);
    }

    public Task<Customer?> GetByEmailAsync(string email)
    {
        return _unitOfWork.CustomerRepository.GetByEmailAsync(email);
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        return _unitOfWork.CustomerRepository.GetAllAsync();
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        var customerExists = await _unitOfWork.CustomerRepository.ExistsByIdAsync(customer.Id);

        if (!customerExists)
        {
            return false;
        }

        _unitOfWork.CustomerRepository.Update(customer);

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var customerExists = await _unitOfWork.CustomerRepository.ExistsByIdAsync(id);

        if (!customerExists)
        {
            return false;
        }

        await _unitOfWork.CustomerRepository.DeleteByIdAsync(id);

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }
}