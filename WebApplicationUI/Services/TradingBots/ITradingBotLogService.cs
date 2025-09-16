namespace WebApplicationUI.Services.TradingBots
{
    public interface ITradingBotLogService
    {
        event Action<string>? LogReceived;

        Task Connect(string botId);

        Task Disconnect(string botId);
    }
}
