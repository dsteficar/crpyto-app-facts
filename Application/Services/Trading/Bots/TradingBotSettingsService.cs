using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.Entity.Authentication;
using Domain.Entity.Trading.Bots;
using Domain.Entity.Users;

namespace Application.Services.Trading.Bots
{
    public class TradingBotSettingsService : GenericService<TradingBotSettings>, ITradingBotSettingsService
    {
        private readonly ITradeBotSettingsRepository _tradeBotSettingsRepository;

        public TradingBotSettingsService(ITradeBotSettingsRepository tradeBotSettingsRepository) : base(tradeBotSettingsRepository)
        {
            _tradeBotSettingsRepository = tradeBotSettingsRepository;
        }

        public async Task<ServiceResult<TradingBotSettings>> GetByTradeBotTaskIdAsync(int id)
        {
            try
            {
                var getResult = await _tradeBotSettingsRepository.GetByTradeBotTaskIdAsync(id);

                return getResult != null
                        ? ServiceResult<TradingBotSettings>.Success(getResult)
                        : ServiceResult<TradingBotSettings>.Failure($"Trade bot settings for trade bot task with ID {id} not found.");
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
