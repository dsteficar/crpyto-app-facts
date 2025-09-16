using Microsoft.AspNetCore.SignalR;

namespace WebAdminUI.Hubs
{
    public class TradingBotLogHub : Hub
    {
        public async Task SendLog(string botId, string logMessage)
        {
            // Broadcast log messages to all connected clients for a specific bot
            await Clients.Group(botId).SendAsync("ReceiveLog", logMessage);
        }

        public async Task JoinBotGroup(string botId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, botId);

            // Send historical logs upon joining the group
            string logFilePath = Path.Combine("Logs", $"{botId}.log");
            if (File.Exists(logFilePath))
            {
                var logLines = await File.ReadAllLinesAsync(logFilePath);
                foreach (var line in logLines)
                {
                    await Clients.Caller.SendAsync("ReceiveLog", line);
                }
            }
        }

        public Task LeaveBotGroup(string botId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, botId);
        }
    }
}
