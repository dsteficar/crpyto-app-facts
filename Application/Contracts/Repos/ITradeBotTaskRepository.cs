using Domain.Entity.Trading.Bots;

namespace Application.Contracts.Repos
{
    public interface ITradeBotTaskRepository : IGenericRepository<TradingBotTask>
    {
        Task<IEnumerable<TradingBotTask>> GetPagedByUserId(int userId, int skip, int top);
        Task<TradingBotTask?> GetByIdEagerAsync(int id);
        Task<IEnumerable<TradingBotTask>> GetEnabledAsync();
        Task<int> GetCountByUserIdAsync(int userId);
    }
}
