using Application.DTOs.Response;
using Domain.Entity.Trading.Bots;

namespace WebAdminUI.Services.Bots
{
    public interface ITradeBotScheduledTaskService
    {
        Task<ServiceResult<IEnumerable<TradingBotTask>>> GetEnabledTasksAsync();
        Task ExecuteTask(CancellationToken cancelToken, TradingBotTask tradingBotTask);
    }
}
