using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.Entity.Trading.Bots;
using Domain.Entity.Users;
using Microsoft.AspNetCore.SignalR;
using WebAdminUI.Hubs;

namespace WebAdminUI.Services.Bots
{
    public class TradeBotScheduledTaskService : ITradeBotScheduledTaskService
    {
        private readonly ILogger<TradeBotScheduledTaskService> _logger;
        private readonly ITradingBotTaskService _tradingBotTaskService;
        private readonly IHubContext<TradingBotLogHub> _hubContext;

        public TradeBotScheduledTaskService(
            ILogger<TradeBotScheduledTaskService> logger,
            ITradingBotTaskService tradingBotTaskService,
            IHubContext<TradingBotLogHub> hubContext)
        {
            _logger = logger;
            _tradingBotTaskService = tradingBotTaskService;
            _hubContext = hubContext;
        }

        public async Task<ServiceResult<IEnumerable<TradingBotTask>>> GetEnabledTasksAsync()
        {

            try
            {
                var getEnabledTradingBotTasksResult = await _tradingBotTaskService.GetEnabledAsync();

                if (!getEnabledTradingBotTasksResult.IsSuccess) return ServiceResult<IEnumerable<TradingBotTask>>.Failure("No enabled trade bot tasks found.");

                var enabledTradingBotTasks = getEnabledTradingBotTasksResult.Value;

                return ServiceResult<IEnumerable<TradingBotTask>>.Success(enabledTradingBotTasks, "Enabled trade bot tasks successfully retireved.");

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

        public async Task ExecuteTask(CancellationToken cancelToken, TradingBotTask tradingBotTask)
        {
            string logFilePath = Path.Combine("Logs", $"{tradingBotTask.Id}.log");

            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath) ?? string.Empty);

            while (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    // Check if the log file exists, otherwise create it
                    if (!File.Exists(logFilePath))
                    {
                        using (var fs = File.Create(logFilePath)) { }
                    }

                    // Append new log entry and send it to connected clients
                    string newLogEntry = $"Log entry at {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    await AppendLogAndSendToClients(logFilePath, tradingBotTask.Id, newLogEntry);

                }
                catch (Exception ex)
                {
                    // Handle any exceptions (e.g., logging)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            //await Task.Delay(1000); // Simulate task execution
        }

        private async Task AppendLogAndSendToClients(string logFilePath, int botId, string logMessage)
        {
            // Append the log message to the file
            await File.AppendAllTextAsync(logFilePath, logMessage + Environment.NewLine);

            // Send the log message to the SignalR group
            await _hubContext.Clients.Group(botId.ToString()).SendAsync("ReceiveLog", logMessage);
        }

    }
}
