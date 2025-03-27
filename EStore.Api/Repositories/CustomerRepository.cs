using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class CustomerRepository(EStoreContext eStoreContext) : ICustomerRepository
{
    public void Create(Customer customer)
    {
        eStoreContext.Customers.Add(customer);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var customer = await eStoreContext.Customers.SingleOrDefaultAsync(c => c.Id == id);

        return customer;
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        var customer = await eStoreContext.Customers.SingleOrDefaultAsync(c => c.Email == email);

        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await eStoreContext.Customers.ToListAsync();
    }

    public void Update(Customer customer)
    {
        eStoreContext.Update(customer);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var customer = await eStoreContext.Customers.SingleOrDefaultAsync(c => c.Id == id);

        eStoreContext.Remove(customer);
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        bool customerExists = await eStoreContext.Customers.AnyAsync(c => c.Id == id);

        return customerExists;
    }

}