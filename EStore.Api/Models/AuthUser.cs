using Microsoft.AspNetCore.Identity;

namespace EStore.Api.Models
{
    public class AuthUser : IdentityUser
    {
        public Guid? CustomerId { get; set; }
    }
}
