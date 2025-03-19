using EStore.Api.Models;

namespace EStore.Api.Services;

public interface IOrderService
{
    Task<bool> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetAllCustomersOrdersAsync(Guid customerId);
    Task<bool> DeleteByIdAsync(Guid id);
    Task<bool> ExistsByIdAsync(Guid id);
}