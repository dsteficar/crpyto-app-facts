using Application.DTOs.TradeBots.Requests;
using Application.DTOs.TradeBots.Responses;

namespace WebApplicationUI.Services.TradingBots
{
    public interface ITradingBotClientService
    {
        Task<GetPagedUserTradingBotsDTO> GetPagedUserTradingBotsAsync(int userId, int skip, int top, CancellationToken cancelToken);
        Task<GetUserTradingBotSettingsResponseDTO> GetUserTradingBotSettingsAsync(int settingId, CancellationToken cancelToken);
        Task<AddUserTradeBotResponseDTO> AddUserTradingBotAsync(AddUserTradeBotRequestDTO request, CancellationToken cancelToken);
        Task<UpdateUserTradeBotResponseDTO> UpdateTradingBotAsync(UpdateUserTradeBotRequestDTO request, CancellationToken cancelToken);
        Task DeleteTradingBotAsync(int id, CancellationToken cancelToken);
    }
}
