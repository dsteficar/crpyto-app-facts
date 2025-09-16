using Application.DTOs.Account.Responses;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text;
using System.Text.Json;

namespace WebAdminUI.Services.Tokens
{
    public class TokenClientService : ITokenClientService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public TokenClientService(ProtectedLocalStorage protectedLocalStorage, HttpClient httpClient)
        {
            _protectedLocalStorage = protectedLocalStorage;
            _httpClient = httpClient;
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            var result = await _protectedLocalStorage.GetAsync<string>("AccessToken");
            return result.Success ? result.Value : null;
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            var result = await _protectedLocalStorage.GetAsync<string>("RefreshToken");
            return result.Success ? result.Value : null;
        }

        public async Task SetAccessTokenAsync(string token)
        {
            await _protectedLocalStorage.SetAsync("AccessToken", token);
        }

        public async Task SetRefreshTokenAsync(string token)
        {
            await _protectedLocalStorage.SetAsync("RefreshToken", token);
        }

        public async Task RefreshTokensAsync()
        {

            var refreshToken = await GetRefreshTokenAsync();

            if (string.IsNullOrEmpty(refreshToken)) return;

            var requestData = new { RefreshToken = refreshToken };
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/refresh-token", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetRefreshTokenResponseDTO>();
                if (result != null)
                {
                    await SetAccessTokenAsync(result.AccessToken);
                    await SetRefreshTokenAsync(result.RefreshToken);
                }
            }
        }
    }
}
