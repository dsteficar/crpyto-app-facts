using Microsoft.AspNetCore.Components;
using System.Net;
using WebApplicationUI.Services.JwtTokens;


namespace WebApplicationUI.Services.Binance
{
    public class BinanceClientService : BaseClientService, IBinanceClientService
    {
        private const string BaseUrl = "api/tradingviewgraph";

        public BinanceClientService(
            HttpClient httpClient,
            IJwtTokenClientService tokenClientService,
            NavigationManager navigationManager) : base(httpClient, tokenClientService, navigationManager)
        {
        }

        public async Task<IEnumerable<string>> GetAllFuturesPairSymbolsAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/futures/symbols/all");

                var response = await SendRequestAsync<IEnumerable<string>>(request);

                if (response.StatusCode == HttpStatusCode.OK) {
                    IEnumerable<string> content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return Enumerable.Empty<string>();
        }
    }
}
