namespace EStore.Contracts.Responses;

public class OrderResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public IEnumerable<OrderProductResponse> Products { get; init; } = [];
}