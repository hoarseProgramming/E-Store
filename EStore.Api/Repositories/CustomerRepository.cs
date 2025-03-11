using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class CustomerRepository(EStoreContext eStoreContext) : ICustomerRepository
{
    public async Task<bool> CreateAsync(Customer customer)
    {
        eStoreContext.Customers.Add(customer);

        await eStoreContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var customer = await eStoreContext.Customers.SingleOrDefaultAsync(c => c.Id == id);

        return customer;
    }

    public Task<Customer?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await eStoreContext.Customers.ToListAsync();
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        eStoreContext.Update(customer);

        var result = await eStoreContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var customer = await eStoreContext.Customers.SingleOrDefaultAsync(c => c.Id == id);
        
        eStoreContext.Remove(customer);

        var result = await eStoreContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        bool customerExists = await eStoreContext.Customers.AnyAsync(c => c.Id == id);

        return customerExists;
    }
}