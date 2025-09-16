using Application.Contracts.Services;
using Application.DTOs.Response;

namespace Application.Services.Trading.Graphs
{
    public class TradingDataService : ITradingDataService
    {
        private readonly IBinanceService _binanceService;

        public TradingDataService(IBinanceService binanceService)
        {
            _binanceService = binanceService;
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetAllFuturesPairsAsync()
        {
            return await _binanceService.GetAllFuturesPairsAsync();
        }
    }
}
