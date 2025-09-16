using Application.DTOs.TradeBots.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Security.Claims;

namespace WebApplicationUI.Pages.Trading.Bots
{
    public partial class TradingBotListPage
    {
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        private IEnumerable<GetUserTradeBotResponseDTO> tradeBots = Enumerable.Empty<GetUserTradeBotResponseDTO>();
        private int userId;
        private int totalCount;
        private bool isLoading;

        private async Task LoadTradeBots(LoadDataArgs args)
        {
            isLoading = true;

            try
            {
                var authState = await AuthenticationStateTask!;
                var user = authState.User;

                if (user.Identity?.IsAuthenticated == true && !user.IsInRole("Anonymous"))
                {
                    var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var parsedUserId))
                    {
                        userId = parsedUserId;

                        var cancelToken = new CancellationToken();
                        var result = await TradingBotClientService.GetPagedUserTradingBotsAsync(userId, (int)args.Skip!, (int)args.Top!, cancelToken);

                        tradeBots = result.Bots;
                        totalCount = result.TotalCount;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading trade bots: {ex.Message}");
            }

            isLoading = false;
        }

        private void EditTradeBot(int id)
        {
            NavigationManager.NavigateTo($"/trade-bots/manage/{id}");
        }

        private void CreateNewTradeBot()
        {
            NavigationManager.NavigateTo("/trade-bots/manage/");
        }

        private async Task DeleteTradeBot(int id)
        {
            var cancelToken = new CancellationToken();
            await TradingBotClientService.DeleteTradingBotAsync(id, cancelToken);
            await LoadTradeBots(new LoadDataArgs { Skip = 0, Top = 5 }); // Refresh the list
        }
    }
}
