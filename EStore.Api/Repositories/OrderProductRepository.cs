using EStore.Api.Database;
using EStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Api.Repositories;

public class OrderProductRepository(EStoreContext eStoreContext) :IOrderProductRepository
{
    public void Create(OrderProduct order_Product)
    {
        eStoreContext.OrderProducts.Add(order_Product);
    }
}