namespace EStore.Contracts.Responses
{
    public class UserInfoResponse
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public IEnumerable<Role>? Roles { get; set; }
    }
    public record Role(string Name);
}
