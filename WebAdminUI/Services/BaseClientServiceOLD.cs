using Domain.DTOs.Common;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using WebAdminUI.Services.Tokens;

namespace WebAdminUI.Services
{
    public abstract class BaseClientServiceOLD
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ITokenClientService _tokenService;

        protected BaseClientServiceOLD(HttpClient httpClient, ITokenClientService tokenService, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _navigationManager = navigationManager;
        }

        protected async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            await AddAuthorizationHeaderAsync(request);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var isTokenRefreshed = await RefreshTokensAsync();

                if (isTokenRefreshed)
                {
                    var clonedRequest = await CloneHttpRequestMessageAsync(request);
                    await AddAuthorizationHeaderAsync(clonedRequest);

                    response = await _httpClient.SendAsync(clonedRequest);
                }
            }

            return response;
        }

        protected async Task<ApiResult<T>> SendRequestAsync<T>(HttpRequestMessage request)
        {

            var response = await SendRequestAsync(request);

            if (response.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(string))
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    return ApiResult<T>.Success((T)(object)stringData);
                }
                else
                {
                    var data = await response.Content.ReadFromJsonAsync<T>();
                    return ApiResult<T>.Success(data!);
                }

                //var data = await response.Content.ReadFromJsonAsync<T>();
                //return ApiResponse<T>.Success(data!);
            }

            var errorMessage = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {

                _navigationManager.NavigateTo("/login");
            }

            return ApiResult<T>.Failure(errorMessage, response.StatusCode);
        }

        private async Task AddAuthorizationHeaderAsync(HttpRequestMessage request)
        {
            var accessToken = await _tokenService.GetAccessTokenAsync();
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        private async Task<bool> RefreshTokensAsync()
        {
            //var refreshToken = await _tokenService.GetRefreshTokenAsync();

            //if (string.IsNullOrEmpty(refreshToken))
            //{
            //    _navigationManager.NavigateTo("/login");
            //    return false;
            //}

            //GetRefreshTokenRequestDTO refreshTokenDTO = new GetRefreshTokenRequestDTO(refreshToken);

            //var request = new HttpRequestMessage(HttpMethod.Post, "api/account/refresh-token")
            //{
            //    Content = new StringContent(JsonSerializer.Serialize(refreshTokenDTO), System.Text.Encoding.UTF8, "application/json")
            //};

            //var response = await _httpClient.SendAsync(request);

            //if(response.StatusCode != HttpStatusCode.OK) return false;

            //var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

            //if (result != null && !string.IsNullOrEmpty(result.RefreshToken) && !string.IsNullOrEmpty(result.AccessToken))
            //{
            //    await _tokenService.SetAccessTokenAsync(result.AccessToken);
            //    await _tokenService.SetRefreshTokenAsync(result.RefreshToken);
            //}

            return true;
        }

        private async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri)
            {
                Content = await CloneHttpContentAsync(request.Content),
                Version = request.Version
            };

            foreach (var header in request.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            foreach (var property in request.Properties)
            {
                clone.Properties[property.Key] = property.Value;
            }

            return clone;
        }

        private async Task<HttpContent?> CloneHttpContentAsync(HttpContent? content)
        {
            if (content == null)
            {
                return null;
            }

            var clone = new MemoryStream();
            await content.CopyToAsync(clone);
            clone.Position = 0;

            var clonedContent = new StreamContent(clone);
            foreach (var header in content.Headers)
            {
                clonedContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clonedContent;
        }
    }
}
