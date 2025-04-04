﻿namespace EStore.Contracts.Responses;

public class ProductResponse
{
    public required int ProductNumber { get; init; }
    public required string ProductName { get; init; }
    public required string Description { get; init; }
    public required double Price { get; init; }
    public string? ImageUrl { get; set; }
    public Guid? CategoryId { get; init; }
    public CategoryResponse? Category { get; init; }
    public required bool IsInAssortment { get; init; }
}