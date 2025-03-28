using EStore.Api.Models;
using EStore.Api.UnitOfWork;

namespace EStore.Api.Services
{
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        public async Task<bool> UpdateAsync(AuthUser user)
        {
            var updated = await unitOfWork.UserRepository.UpdateAsync(user);

            return updated ? true : false;
        }
    }
}
