namespace EStore.Contracts.Requests;

public class CreateOrderProductRequest
{
    public required int ProductNumber { get; init; }
    public int Quantity { get; set; } = 1;
}