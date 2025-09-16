using Application.DTOs.Account.Requests;
using Application.DTOs.Account.Responses;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace WebApplicationUI.Services.JwtTokens
{
    public class JwtTokenClientService : IJwtTokenClientService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public JwtTokenClientService(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            var result = await _localStorageService.GetItemAsync<string>("AccessToken");
            return result;
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            var result = await _localStorageService.GetItemAsync<string>("RefreshToken");
            return result;
        }

        public async Task SetAccessTokenAsync(string token)
        {
            await _localStorageService.SetItemAsync("AccessToken", token);
        }

        public async Task SetRefreshTokenAsync(string token)
        {
            await _localStorageService.SetItemAsync("RefreshToken", token);
        }

        public async Task DeleteAccessTokenAsync()
        {
            await _localStorageService.RemoveItemAsync("AccessToken");
        }

        public async Task DeleteRefreshTokenAsync()
        {
            await _localStorageService.RemoveItemAsync("RefreshToken");
        }

        public async Task<bool> RefreshTokensAsync()
        {
            var refreshToken = await GetRefreshTokenAsync();

            if (string.IsNullOrEmpty(refreshToken)) return false;

            var requestData = new GetRefreshTokenRequestDTO();
            requestData.RefreshToken = refreshToken;

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            // Send the POST request
            try
            {
                var response = await _httpClient.PostAsync("api/account/refresh-token", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<GetRefreshTokenResponseDTO>();
                    if (result != null)
                    {
                        await SetAccessTokenAsync(result.AccessToken);
                        await SetRefreshTokenAsync(result.RefreshToken);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                var test = "";
            }
            return false;        
        }
    }
}
