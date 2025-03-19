using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface IOrderRepository
{
    void Create(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetAllCustomersOrdersAsync(Guid customerId);
    Task DeleteByIdAsync(Guid id);
    Task<bool> ExistsByIdAsync(Guid id);
}