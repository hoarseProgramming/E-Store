using EStore.Api.Models;

namespace EStore.Api.Services
{
    public interface IUserService
    {
        public Task<bool> UpdateAsync(AuthUser user);
    }
}
