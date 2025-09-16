using Application.DTOs.Admin.Account.Requests;
using Application.DTOs.Admin.Account.Responses;
using Microsoft.AspNetCore.Components;

namespace WebAdminUI.Services.Accounts
{
    public class AccountClientService : IAccountClientService
    {
        private const string BaseUrl = "api/account";
        private readonly HttpClient _httpClient;

        public AccountClientService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginAdminResponseDTO> LoginAsync(LoginAdminRequestDTO requestDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", requestDto);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<LoginAdminResponseDTO>();
                    return content != null ? content : null!;
                }
                else
                {
                    Console.WriteLine($"Login failed with status code: {response.StatusCode}");
                    return null!;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                return null!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null!;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync($"{BaseUrl}/logout", null);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User successfully logged out.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Logout failed with status code: {response.StatusCode}");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}