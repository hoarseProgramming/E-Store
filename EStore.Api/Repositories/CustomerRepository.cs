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
        var customerExists = await eStoreContext.Customers.AnyAsync(c => c.Id == customer.Id);

        if (!customerExists)
        {
            return false;
        }
        
        eStoreContext.Update(customer);

        var result = await eStoreContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var customer = await eStoreContext.Customers.SingleOrDefaultAsync(c => c.Id == id);

        if (customer is null)
        {
            return false;
        }
        
        eStoreContext.Remove(customer);

        await eStoreContext.SaveChangesAsync();
        return true;
    }
}