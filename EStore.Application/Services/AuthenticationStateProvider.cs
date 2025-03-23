using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using EStore.Application.Dtos;
using EStore.Application.Events;
using EStore.Application.Interfaces;

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
        var result = await _httpClient.GetAsync("api/manage/info");

        return result.IsSuccessStatusCode;
    }

    public async Task<string> GetRoleAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("eStoreToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var result = await _httpClient.GetAsync("api/roles");

        if (result.IsSuccessStatusCode)
        {
            var roles = await result.Content.ReadFromJsonAsync<List<GetRolesResponse>>();

            if (roles.Any())
            {
                return roles.FirstOrDefault().value;
            }
        }

        return "";
    }
}