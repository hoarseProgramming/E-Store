namespace EStore.Api.Models;

public class Customer
{
    public Guid Id { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public required string ZipCode { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}