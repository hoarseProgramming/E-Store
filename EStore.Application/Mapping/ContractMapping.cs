using EStore.Application.ApplicationModels;
using EStore.Contracts.Requests;
using EStore.Contracts.Responses;

namespace EStore.Application.Mapping
{
    public static class ContractMapping
    {
        public static StoreProduct MapToStoreProduct(this ProductResponse response)
        {
            return new StoreProduct
            {
                ProductNumber = response.ProductNumber,
                ProductName = response.ProductName,
                Description = response.Description,
                Price = response.Price,
                ImageUrl = response.ImageUrl is null ? "" : response.ImageUrl,
                CategoryId = response.CategoryId,
                Category = response.Category.MapToStoreCategory(),
                IsInAssortment = response.IsInAssortment
            };
        }

        public static IEnumerable<StoreProduct> MapToStoreProducts(this ProductsResponse response)
        {
            return response.Products.Select(MapToStoreProduct);
        }


        public static IEnumerable<CreateOrderProductRequest> MapToRequest(this IEnumerable<StoreProduct> products)
        {
            return products.Select(MapToCreateOrderProductRequest);
        }

        public static CreateOrderProductRequest MapToCreateOrderProductRequest(this StoreProduct product)
        {
            return new CreateOrderProductRequest()
            {
                ProductNumber = product.ProductNumber,
                Quantity = product.Quantity
            };
        }

        public static CreateProductRequest MapToCreateProductRequest(this StoreProduct product)
        {
            return new CreateProductRequest()
            {
                ProductNumber = product.ProductNumber,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                IsInAssortment = product.IsInAssortment,
                CategoryId = product.CategoryId
            };
        }

        public static UpdateProductRequest MapToUpdateProductRequest(this StoreProduct product)
        {
            return new UpdateProductRequest()
            {
                ProductNumber = product.ProductNumber,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                IsInAssortment = product.IsInAssortment,
                CategoryId = product.CategoryId
            };
        }

        public static AppCustomer MapToAppCustomer(this CustomerResponse response)
        {
            return new AppCustomer()
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                Email = response.Email,
                Address = response.Address,
                ZipCode = response.ZipCode,
                City = response.City,
                Country = response.Country,
                Orders = response.Orders
            };
        }

        public static IEnumerable<AppCustomer> MapToAppCustomers(this CustomersResponse response)
        {
            return response.Customers.Select(MapToAppCustomer);
        }

        public static StoreCategory MapToStoreCategory(this CategoryResponse response)
        {
            return new StoreCategory() { Id = response.Id, CategoryName = response.CategoryName };
        }

        public static IEnumerable<StoreCategory> MapToStoreCategories(this CategoriesResponse response)
        {
            return response.Categories.Select(MapToStoreCategory);
        }
    }
}
