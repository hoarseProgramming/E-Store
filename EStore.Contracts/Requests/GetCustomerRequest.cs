namespace EStore.Contracts.Requests
{
    public class GetCustomerRequest
    {
        public required Guid Id { get; set; }
        public bool IsUserId { get; set; }
    }
}
