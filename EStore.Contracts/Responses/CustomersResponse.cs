namespace EStore.Contracts.Responses;

public class CustomersResponse
{
    public IEnumerable<CustomerResponse> Customers { get; init; } = [];
}