namespace EStore.Api.Models;

public class OrderProduct
{
    public required Guid OrderId { get; init; }
    public required int ProductNumber { get; init; }
    public int Quantity { get; set; } = 1;
}