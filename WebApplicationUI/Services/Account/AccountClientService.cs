using Application.DTOs.Account.Requests;
using Application.DTOs.Account.Responses;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;
using WebApplicationUI.Services;
using WebApplicationUI.Services.JwtTokens;

namespace WebAdminUI.Services.Accounts
{

    public class AccountClientService : BaseClientService, IAccountClientService
    {
        private const string BaseUrl = "api/account";
        private readonly IJwtTokenClientService _tokenService;

        public AccountClientService(HttpClient httpClient, IJwtTokenClientService tokenService, NavigationManager navigationManager)
        : base(httpClient, tokenService, navigationManager)
        {
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO requestDto)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/login")
                {
                    Content = JsonContent.Create(requestDto)
                };

                var response = await SendRequestAsync<LoginResponseDTO>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;

                    if (!string.IsNullOrEmpty(content.AccessToken) && !string.IsNullOrEmpty(content.RefreshToken))
                    {
                        await _tokenService.SetAccessTokenAsync(content.AccessToken);
                        await _tokenService.SetRefreshTokenAsync(content.RefreshToken);
                    }

                    return content;
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

            return null!;
        }

        public async Task<string> LoginWithAccessTokenAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/token-login");

                var response = await SendRequestAsync<string>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
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

            return "";
        }

        public async Task LogoutAsync()
        {
            try
            {

                await _tokenService.DeleteAccessTokenAsync();
                await _tokenService.DeleteRefreshTokenAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO requestDto)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/register")
                {
                    Content = JsonContent.Create(requestDto)
                };
                var response = await SendRequestAsync<RegisterResponseDTO>(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
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

            return null!;
        }
    }
}
