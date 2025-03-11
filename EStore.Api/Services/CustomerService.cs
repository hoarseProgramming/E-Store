using EStore.Api.Models;
using EStore.Api.Repositories;

namespace EStore.Api.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public Task<bool> CreateAsync(Customer customer)
    {
        return _customerRepository.CreateAsync(customer);
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return _customerRepository.GetByIdAsync(id);
    }

    public Task<Customer?> GetByEmailAsync(string email)
    {
        return _customerRepository.GetByEmailAsync(email);
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        return _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> UpdateAsync(Customer customer)
    {
        var movieExists = await _customerRepository.ExistsByIdAsync(customer.Id);

        if (!movieExists)
        {
            return null;
        }

        var updated = await _customerRepository.UpdateAsync(customer);

        return updated ? customer : null;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var movieExists = await _customerRepository.ExistsByIdAsync(id);

        if (!movieExists)
        {
            return false;
        }

        return await _customerRepository.DeleteByIdAsync(id);
    }
}