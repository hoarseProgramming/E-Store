using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class OrderRepository(EStoreContext eStoreContext) : IOrderRepository
{
    private readonly EStoreContext _eStoreContext = eStoreContext;

    public void Create(Order order)
    {
        _eStoreContext.Orders.Add(order);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _eStoreContext.Orders
            .Include(o => o.OrderProducts)
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _eStoreContext.Orders
            .Include(o => o.OrderProducts)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllCustomersOrdersAsync(Guid customerId)
    {
        return await _eStoreContext.Orders
            .Include(o => o.OrderProducts)
            .Where(o => o.CustomerId == customerId).ToListAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var order = await _eStoreContext.Orders.SingleOrDefaultAsync(o => o.Id == id);

        eStoreContext.Remove(order);
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        var productExists = await _eStoreContext.Orders.AnyAsync(o => o.Id == id);

        return productExists;
    }
}