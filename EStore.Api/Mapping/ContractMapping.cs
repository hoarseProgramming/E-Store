using EStore.Api.Models;
using EStore.Contracts.Requests;
using EStore.Contracts.Responses;

namespace EStore.Api.Mapping;

public static class ContractMapping
{
    public static Product MapToProduct(this CreateProductRequest request)
    {
        return new Product()
        {
            ProductNumber = request.ProductNumber,
            ProductName = request.ProductName,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId,
            IsInAssortment = request.IsInAssortment
        };
    }
    
    public static Product MapToProduct(this UpdateProductRequest request, int productNumber)
    {
        return new Product()
        {
            ProductNumber = productNumber,
            ProductName = request.ProductName,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId,
            IsInAssortment = request.IsInAssortment
        };
    }

    public static ProductResponse MapToResponse(this Product product)
    {
        return new ProductResponse()
        {
            ProductNumber = product.ProductNumber,
            ProductName = product.ProductName,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            IsInAssortment = product.IsInAssortment
        };
    }

    public static ProductsResponse MapToResponse(this IEnumerable<Product> products)
    {
        return new ProductsResponse()
        {
            Products = products.Select(MapToResponse)
        };
    }

    public static Customer MapToCustomer(this CreateCustomerRequest request)
    {
        return new Customer()
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Address = request.Address,
            ZipCode = request.ZipCode,
            City = request.City,
            Country = request.Country
        };
    }

    public static CustomerResponse MapToResponse(this Customer customer)
    {
        return new CustomerResponse()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Address = customer.Address,
            ZipCode = customer.ZipCode,
            City = customer.City,
            Country = customer.Country
        };
    }

    public static IEnumerable<CustomerResponse> MapToResponse(this IEnumerable<Customer> customers)
    {
        return customers.Select(MapToResponse);
    }

    public static Customer MapToCustomer(this UpdateCustomerRequest request, Guid id)
    {
        return new Customer()
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Address = request.Address,
            ZipCode = request.ZipCode,
            City = request.City,
            Country = request.Country
        };
    }

    public static Category MapToCategory(this CreateCategoryRequest request)
    {
        return new Category()
        {
            Id = Guid.NewGuid(),
            CategoryName = request.CategoryName
        };
    }

    public static CategoryResponse MapToResponse(this Category category)
    {
        return new CategoryResponse()
        {
            Id = category.Id,
            CategoryName = category.CategoryName
        };
    }

    public static IEnumerable<CategoryResponse> MapToResponse(this IEnumerable<Category> categories)
    {
        return categories.Select(MapToResponse);
    }
}