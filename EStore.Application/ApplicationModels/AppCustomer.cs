using EStore.Contracts.Responses;

namespace EStore.Application.ApplicationModels
{
    public class AppCustomer
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string ZipCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required List<OrderResponse> Orders { get; set; } = [];
    }
}
