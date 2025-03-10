namespace EStore.Contracts.Responses;

public class ProductsResponse
{
    public IEnumerable<ProductResponse> Products { get; init; } = [];
}