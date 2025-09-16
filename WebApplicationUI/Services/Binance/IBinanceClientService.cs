using Application.DTOs;

namespace WebApplicationUI.Services.Binance
{
    public interface IBinanceClientService
    {
        Task<IEnumerable<string>> GetAllFuturesPairSymbolsAsync();
    }
}
