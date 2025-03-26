namespace EStore.Application.ApplicationModels
{
    public class StoreProduct
    {
        public required int ProductNumber { get; init; }
        public required string ProductName { get; init; }
        public required string Description { get; init; }
        public required double Price { get; init; }
        public int Quantity { get; set; } = 1;
        public Guid? CategoryId { get; init; }
        public required bool IsInAssortment { get; init; }
    }
}
