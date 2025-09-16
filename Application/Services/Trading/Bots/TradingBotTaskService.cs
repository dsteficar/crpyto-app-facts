using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.Entity.Trading.Bots;

namespace Application.Services.Trading.Bots
{
    public class TradingBotTaskService : GenericService<TradingBotTask>, ITradingBotTaskService
    {
        private readonly ITradeBotTaskRepository _tradeBotTaskRepository;
        private readonly IAccountService _accountService;

        public TradingBotTaskService(
            ITradeBotTaskRepository tradeBotTaskRepository,
            IAccountService accountService
            ) : base(tradeBotTaskRepository)
        {
            _tradeBotTaskRepository = tradeBotTaskRepository;
            _accountService = accountService;
        }

        public async Task<ServiceResult<IEnumerable<TradingBotTask>>> GetPagedByUserIdAsync(int id, int skip, int top)
        {
            try
            {
                var userTradeBotTasks = await _tradeBotTaskRepository.GetPagedByUserId(id, skip, top);

                if (userTradeBotTasks == null || !userTradeBotTasks.Any())
                    return ServiceResult<IEnumerable<TradingBotTask>>.Failure("No trade bot tasks found for user.");
                else
                    return ServiceResult<IEnumerable<TradingBotTask>>.Success(userTradeBotTasks, "Trade bot tasks successfully retireved.");
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

        public async Task<ServiceResult<TradingBotTask>> GetByIdEagerAsync(int id)
        {
            try
            {
                var userTradeBotTask = await _tradeBotTaskRepository.GetByIdEagerAsync(id);

                if (userTradeBotTask == null)
                    return ServiceResult<TradingBotTask>.Failure("No trade bot tasks found for user.");
                else
                    return ServiceResult<TradingBotTask>.Success(userTradeBotTask, "Trade bot tasks successfully retireved.");
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

        public async Task<ServiceResult<IEnumerable<TradingBotTask>>> GetEnabledAsync()
        {
            try
            {
                var userTradeBotTasks = await _tradeBotTaskRepository.GetEnabledAsync();

                if (userTradeBotTasks == null || !userTradeBotTasks.Any())
                    return ServiceResult<IEnumerable<TradingBotTask>>.Failure("No enabled trade bot tasks found.");
                else
                    return ServiceResult<IEnumerable<TradingBotTask>>.Success(userTradeBotTasks, "Enabled trade bot tasks successfully retireved.");
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

        public async Task<ServiceResult<int>> GetCountByUserIdAsync(int userId)
        {
            try
            {
                var userTradeBotTasksCount = await _tradeBotTaskRepository.GetCountByUserIdAsync(userId);

                return ServiceResult<int>.Success(userTradeBotTasksCount, "Trade bot tasks count successfully retireved.");
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
