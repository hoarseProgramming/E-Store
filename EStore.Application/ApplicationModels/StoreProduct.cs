namespace EStore.Application.ApplicationModels
{
    public class StoreProduct
    {
        public required int ProductNumber { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required double Price { get; set; }
        public int Quantity { get; set; } = 1;
        public Guid? CategoryId { get; set; }
        public StoreCategory? Category { get; set; }
        public required bool IsInAssortment { get; set; }
    }
}
