namespace EStore.Contracts.Requests;

public class UpdateProductRequest
{
    public required int ProductNumber { get; init; }
    public required string ProductName { get; init; }
    public required string Description { get; init; }
    public required double Price { get; init; }
    public required int CategoryId { get; init; }
    public required bool IsInAssortment { get; init; }
}