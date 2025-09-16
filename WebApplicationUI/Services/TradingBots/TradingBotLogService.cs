using Microsoft.AspNetCore.SignalR.Client;

namespace WebApplicationUI.Services.TradingBots
{
    public class TradingBotLogService : ITradingBotLogService
    {
        private HubConnection? _hubConnection;

        public event Action<string>? LogReceived;

        public async Task Connect(string botId)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://your-admin-server-url/tradeBotLogHub")
                .Build();

            _hubConnection.On<string>("ReceiveLog", (log) =>
            {
                LogReceived?.Invoke(log);
            });

            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("JoinBotGroup", botId);
        }

        public async Task Disconnect(string botId)
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.SendAsync("LeaveBotGroup", botId);
                await _hubConnection.StopAsync();
                _hubConnection = null;
            }
        }
    }
}
