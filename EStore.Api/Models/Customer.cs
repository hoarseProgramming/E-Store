namespace EStore.Api.Models;

public class Customer
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Address { get; init; }
    public required string ZipCode { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
    public List<Order> Orders { get; init; } = [];
}