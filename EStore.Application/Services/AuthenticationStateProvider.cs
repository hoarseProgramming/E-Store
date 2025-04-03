using Blazored.LocalStorage;
using EStore.Application.Interfaces;
using EStore.Contracts.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EStore.Application.Services;

public class AuthenticationStateProvider : IAuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationStateProvider(IHttpClientFactory factory, ILocalStorageService localStorage)
    {
        _httpClient = factory.CreateClient("backend");
        _localStorage = localStorage;

    }
    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("eStoreToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetAsync("api/user/info");

        return result.IsSuccessStatusCode;
    }

    public async Task<string> GetRoleAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("eStoreToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/user/info");

        if (response.IsSuccessStatusCode)
        {
            var userInfo = await response.Content.ReadFromJsonAsync<UserInfoResponse>();

            if (userInfo.Roles.Any())
            {
                return userInfo.Roles.FirstOrDefault().Name;
            }
        }

        return "";
    }
}