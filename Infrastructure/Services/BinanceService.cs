using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.ResultObjects.Binance;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class BinanceService : IBinanceService
    {
        private readonly HttpClient _httpClient;

        public BinanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetAllFuturesPairsAsync()
        {
            try
            {
                string endpoint = "fapi/v1/exchangeInfo";
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    var exchangeInfo = JsonSerializer.Deserialize<BinanceExchangeInfoResult>(content);

                    var futuresPairs = exchangeInfo?.Symbols
                        .Where(s => s.ContractType == "PERPETUAL") // Filter for perpetual futures contracts
                        .Select(s => s.SymbolName) // Extract the symbol names
                        .ToList();

                    return ServiceResult<IEnumerable<string>>.Success(futuresPairs, $"Binance pair symbol data successfully fetched.");
                }
                else
                {
                    return ServiceResult<IEnumerable<string>>.Failure($"No binance pair symbol data was fetched.");
                }

            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw; // Let the InfrastructureException propagate up the stack
            }
        }
    }
}
