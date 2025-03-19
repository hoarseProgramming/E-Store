namespace EStore.Api.Models;

public class Product
{
    public required int ProductNumber { get; init; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public Guid? CategoryId { get; set; }
    public required bool IsInAssortment { get; set; }
}