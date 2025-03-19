namespace EStore.Contracts.Responses;

public class OrderProductsResponse
{
    public IEnumerable<OrderProductResponse> OrderProducts { get; init; } = [];
}