using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface ICustomerRepository
{
    void Create(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer?> GetByEmailAsync(string email);
    Task<IEnumerable<Customer>> GetAllAsync();
    void Update(Customer customer);
    Task DeleteByIdAsync(Guid id);
    Task<bool> ExistsByIdAsync(Guid id);
}