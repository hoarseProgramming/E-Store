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

    public static Order MapToOrder(this CreateOrderRequest request)
    {
        var newId = Guid.NewGuid();
        
        return new Order()
        {
            Id = newId,
            CustomerId = request.CustomerId,
            OrderProducts = request.OrderProducts.MapToOrderProducts(newId).ToList()
        };
    }

    public static OrderResponse MapToResponse(this Order order)
    {
        return new OrderResponse()
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Products = order.OrderProducts.MapToResponse()
        };
    }

    public static OrdersResponse MapToResponse(this IEnumerable<Order> orders)
    {
        return new OrdersResponse()
        {
            Orders = orders.Select(MapToResponse)
        };
    }

    public static OrderProduct MapToOrderProduct(CreateOrderProductRequest request, Guid orderId)
    {
        return new OrderProduct()
        {
            OrderId = orderId,
            ProductNumber = request.ProductNumber,
            Quantity = request.Quantity
        };
    }

    public static IEnumerable<OrderProduct> MapToOrderProducts(this IEnumerable<CreateOrderProductRequest> requests,
        Guid orderId)
    {
        return requests.Select(request => MapToOrderProduct(request, orderId));
    }

    public static OrderProductResponse MapToResponse(this OrderProduct orderProduct)
    {
        return new OrderProductResponse()
        {
            ProductNumber = orderProduct.ProductNumber,
            Quantity = orderProduct.Quantity
        };
    }

    public static IEnumerable<OrderProductResponse> MapToResponse(this List<OrderProduct> orderProducts)
    {
        return orderProducts.Select(MapToResponse);
    }
}