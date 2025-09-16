using Application.DTOs.Response;
using Domain.Entity.Trading.Bots;

namespace Application.Contracts.Services
{
    public interface ITradingBotSettingsService : IGenericService<TradingBotSettings>
    {
        Task<ServiceResult<TradingBotSettings>> GetByTradeBotTaskIdAsync(int id);
    }
}