namespace EStore.Api.Models;

public class Category
{
    public Guid Id { get; init; }
    public required string CategoryName { get; set; }
}