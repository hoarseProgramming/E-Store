namespace EStore.Contracts.Requests;

public class CreateOrderRequest
{
    public required Guid CustomerId { get; init; }
    public required List<CreateOrderProductRequest> OrderProducts { get; init; }
}