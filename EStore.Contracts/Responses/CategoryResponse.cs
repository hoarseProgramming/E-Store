namespace EStore.Contracts.Responses;

public class CategoryResponse
{
    public required Guid Id { get; init; }
    public required string CategoryName { get; init; }
}