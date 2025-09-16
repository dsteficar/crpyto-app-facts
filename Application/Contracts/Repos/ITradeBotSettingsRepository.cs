using Domain.Entity.Trading.Bots;

namespace Application.Contracts.Repos
{
    public interface ITradeBotSettingsRepository : IGenericRepository<TradingBotSettings>
    {
       Task<TradingBotSettings> GetByTradeBotTaskIdAsync(int id);
    }
}
