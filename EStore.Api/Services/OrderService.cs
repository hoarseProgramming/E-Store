using EStore.Api.Models;
using EStore.Api.Repositories;
using EStore.Api.UnitOfWork;

namespace EStore.Api.Services;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<bool> CreateAsync(Order order)
    {
        _unitOfWork.OrderRepository.Create(order);

        // foreach (var orderProduct in orderProducts)
        // {
        //     _unitOfWork.OrderProductRepository.Create(orderProduct);
        // }

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.OrderRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _unitOfWork.OrderRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Order>> GetAllCustomersOrdersAsync(Guid customerId)
    {
        return await _unitOfWork.OrderRepository.GetAllCustomersOrdersAsync(customerId);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var orderExists = await ExistsByIdAsync(id);
        
        if (!orderExists)
        {
            return false;
        }
        
        await _unitOfWork.OrderRepository.DeleteByIdAsync(id);
        
        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _unitOfWork.OrderRepository.ExistsByIdAsync(id);
    }
}