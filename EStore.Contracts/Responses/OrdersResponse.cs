namespace EStore.Contracts.Responses;

public class OrdersResponse
{
    public IEnumerable<OrderResponse> Orders { get; init; } = [];
}