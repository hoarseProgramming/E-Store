namespace EStore.Contracts.Requests
{
    public class SetCustomerRequest
    {
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
    }
}
