using Application.DTOs.Response;
using Domain.Entity.Trading.Bots;

namespace Application.Contracts.Services
{
    public interface ITradingBotTaskService : IGenericService<TradingBotTask>
    {
        Task<ServiceResult<IEnumerable<TradingBotTask>>> GetPagedByUserIdAsync(int userId, int skip, int top);

        Task<ServiceResult<TradingBotTask>> GetByIdEagerAsync(int id);

        Task<ServiceResult<IEnumerable<TradingBotTask>>> GetEnabledAsync();

        Task<ServiceResult<int>> GetCountByUserIdAsync(int userId);
    }
}
