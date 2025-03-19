using EStore.Api.Models;

namespace EStore.Api.Repositories;

public interface IOrderProductRepository
{
    public void Create(OrderProduct order_Product);
}