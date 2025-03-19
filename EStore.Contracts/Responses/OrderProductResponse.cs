namespace EStore.Contracts.Responses;

public class OrderProductResponse
{
    public required int ProductNumber { get; init; }
    public int Quantity { get; init; }
}