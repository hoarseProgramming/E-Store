using EStore.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace EStore.Api.Repositories
{
    public class UserRepository(UserManager<AuthUser> userManager) : IUserRepository
    {
        public async Task<bool> UpdateAsync(AuthUser user)
        {
            var result = await userManager.UpdateAsync(user);

            return result.Succeeded ? true : false;
        }
    }
}
