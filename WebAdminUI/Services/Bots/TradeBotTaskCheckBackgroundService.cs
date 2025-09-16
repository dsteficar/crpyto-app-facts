namespace WebAdminUI.Services.Bots
{
    public class TradeBotTaskCheckBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TradeBotTaskCheckBackgroundService> _logger;

        public TradeBotTaskCheckBackgroundService(IServiceProvider serviceProvider, ILogger<TradeBotTaskCheckBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancelToken)
        {
            _logger.LogInformation("ScheduledTaskBackgroundService started.");

            while (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    // Check for tasks every 10 seconds
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var scheduledTaskService = scope.ServiceProvider.GetRequiredService<ITradeBotScheduledTaskService>();

                        // Fetch enabled tasks from database or repository
                        var getTasksToRunResult = await scheduledTaskService.GetEnabledTasksAsync();

                        if(!getTasksToRunResult.IsSuccess) { continue; }

                        var tasksToRun = getTasksToRunResult.Value;

                        foreach (var task in tasksToRun)
                        {
                            Hangfire.BackgroundJob.Enqueue(() => scheduledTaskService.ExecuteTask(cancelToken, task));
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking or triggering tasks.");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), cancelToken);
            }

            _logger.LogInformation("ScheduledTaskBackgroundService stopped.");
        }
    }
}
