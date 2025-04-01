namespace EStore.Application.ApplicationModels
{
    public class StoreCategory
    {
        public required Guid Id { get; init; }
        public required string CategoryName { get; init; }

        public override string ToString() => CategoryName;
    }
}
