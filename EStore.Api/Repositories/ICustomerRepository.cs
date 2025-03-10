using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer?> GetByEmailAsync(string email);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteByIdAsync(Guid id);
}