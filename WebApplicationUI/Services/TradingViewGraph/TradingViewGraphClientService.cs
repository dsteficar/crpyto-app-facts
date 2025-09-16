using Application.DTOs.ParallelChannel.Requests;
using Application.DTOs.ParallelChannel.Responses;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;
using WebApplicationUI.Services.JwtTokens;

namespace WebApplicationUI.Services.TradingViewGraph
{
    public class TradingViewGraphClientService : BaseClientService, ITradingViewGraphClientService
    {
        private const string BaseUrl = "api/tradingviewgraph";

        public TradingViewGraphClientService(
            HttpClient httpClient,
            IJwtTokenClientService tokenClientService,
            NavigationManager navigationManager) : base(httpClient, tokenClientService, navigationManager)
        {
        }

        public async Task<IEnumerable<string>> GetAllFuturesPairSymbolsAsync(CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/futures/symbols/all");

                var response = await SendRequestAsync<IEnumerable<string>>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    IEnumerable<string> content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

            return Enumerable.Empty<string>();
        }

        public async Task<AddParallelChannelResponseDTO> AddParallelChannelForUser(AddParallelChannelRequestDTO dto, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/channels/parallels/add")
                {
                    Content = JsonContent.Create(dto)
                };

                var response = await SendRequestAsync<AddParallelChannelResponseDTO>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

            return null!;
        }

        public async Task UpdateParallelChannel(UpdateParallelChannelRequestDTO dto, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/channels/parallels/update")
                {
                    Content = JsonContent.Create(dto)
                };

                var response = await SendRequestAsync<string>(request);

                return;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

        }

        public async Task DeleteParallelChannel(int parallelChannelId, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseUrl}/channels/parallels/delete/{parallelChannelId}");

                var response = await SendRequestAsync<string>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }
        }

        public async Task<List<GetParallelChannelDTO>> FindChannelsByUserIdAndStartTimestamp(int userId, decimal startTimestamp, CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();
            try
            {
                var requestUrl = $"{BaseUrl}/users/{userId}/channels/parallels?startTimestamp={startTimestamp}";

                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                var response = await SendRequestAsync<List<GetParallelChannelDTO>>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }

            return new List<GetParallelChannelDTO>();
        }
    }
}
