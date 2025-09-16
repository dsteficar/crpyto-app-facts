using Application.DTOs.TradeBots.Requests;
using Application.DTOs.TradeBots.Responses;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;
using WebApplicationUI.Services.JwtTokens;

namespace WebApplicationUI.Services.TradingBots
{

    public class TradingBotClientService : BaseClientService, ITradingBotClientService
    {

        private const string BaseUrl = "api/trading-bots";

        public TradingBotClientService(
            HttpClient httpClient,
            IJwtTokenClientService tokenClientService,
            NavigationManager navigationManager) : base(httpClient, tokenClientService, navigationManager)
        {
        }

        public async Task<GetPagedUserTradingBotsDTO> GetPagedUserTradingBotsAsync(int userId, int skip, int top, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/user/{userId}/{skip}/{top}");

                var response = await SendRequestAsync<GetPagedUserTradingBotsDTO>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

            return null!;
        }

        public async Task<GetUserTradingBotSettingsResponseDTO> GetUserTradingBotSettingsAsync(int settingId, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/settings/{settingId}");

                var response = await SendRequestAsync<GetUserTradingBotSettingsResponseDTO>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

            return null!;
        }

        public async Task<AddUserTradeBotResponseDTO> AddUserTradingBotAsync(AddUserTradeBotRequestDTO request, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/add")
                {
                    Content = JsonContent.Create(request)
                };

                var response = await SendRequestAsync<AddUserTradeBotResponseDTO>(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

            return null!;
        }

        public async Task<UpdateUserTradeBotResponseDTO> UpdateTradingBotAsync(UpdateUserTradeBotRequestDTO request, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/update")
                {
                    Content = JsonContent.Create(request)
                };

                var response = await SendRequestAsync<UpdateUserTradeBotResponseDTO>(httpRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

            return null!;
        }

        public async Task DeleteTradingBotAsync(int id, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/delete/{id}");

                var response = await SendRequestAsync<string>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Trade bot deleted successfully.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }
        }
    }
}
