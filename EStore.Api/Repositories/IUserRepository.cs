using EStore.Api.Models;

namespace EStore.Api.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> UpdateAsync(AuthUser user);
    }
}
