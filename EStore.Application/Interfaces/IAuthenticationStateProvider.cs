namespace EStore.Application.Interfaces;

public interface IAuthenticationStateProvider
{
    Task<bool> IsAuthenticatedAsync();
    Task<string> GetRoleAsync();
}