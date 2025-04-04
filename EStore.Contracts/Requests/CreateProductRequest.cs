namespace EStore.Contracts.Requests;

public class CreateProductRequest
{
    public required int ProductNumber { get; init; }
    public required string ProductName { get; init; }
    public required string Description { get; init; }
    public required double Price { get; init; }
    public string? ImageUrl { get; set; }
    public Guid? CategoryId { get; init; }
    public required bool IsInAssortment { get; init; }
}