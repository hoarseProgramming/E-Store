namespace EStore.Api.Models;

public class Order
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public List<OrderProduct> OrderProducts { get; init; } = [];
}