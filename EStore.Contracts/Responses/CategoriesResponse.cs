namespace EStore.Contracts.Responses;

public class CategoriesResponse
{
    public IEnumerable<CategoryResponse> Categories { get; init; } = [];
}