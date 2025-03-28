using EStore.Application.ApplicationModels;
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
                CategoryId = response.CategoryId,
                IsInAssortment = response.IsInAssortment
            };
        }

        public static IEnumerable<StoreProduct> MapToStoreProducts(this ProductsResponse response)
        {
            return response.Products.Select(MapToStoreProduct);
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
                Country = response.Country
            };
        }
    }
}
