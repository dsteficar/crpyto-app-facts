using Application.DTOs.TradeBots.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using WebApplicationUI.Models.Trading.Bots;

namespace WebApplicationUI.Pages.Trading.Bots
{
    public partial class TradingBotManagerPage
    {
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        private TradingBotEditFormModel FormModel = new TradingBotEditFormModel();

        [Parameter] public int? Id { get; set; } // Nullable for "new"
        private bool isEditMode => Id.HasValue; // Determines mode


        private List<DropDownItem<TradeBotMarketType>> MarketTypes { get; set; } = new();
        private List<DropDownItem<TradeBotMarketDirection>> MarketDirections { get; set; } = new();
        private List<DropDownItem<TradeBotChannelStructureType>> ChannelStructures { get; set; } = new();
        private List<DropDownItem<TradeBotChannelInfinityType>> ChannelInfinities { get; set; } = new();
        private List<DropDownItem<TradeBotSlotType>> TradeBotSlotTypes { get; set; } = new();
        private List<DropDownItem<TradeBotQuantityType>> QuantityTypes { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            PopulateDropdownMenusWithEnumerationValues();

            if (isEditMode)
            {
                var cancelToken = new CancellationToken();
                var tradeBot = await TradeBotClientService.GetUserTradingBotSettingsAsync(Id!.Value, cancelToken);
                if (tradeBot != null)
                {
                    FormModel = Mapper.Map<TradingBotEditFormModel>(tradeBot);
                }
            }
        }

        private async Task Submit(TradingBotEditFormModel model)
        {
            try
            {
                var cancelToken = new CancellationToken();

                if (isEditMode)
                {
                    // Update Trade Bot
                    var updateRequest = Mapper.Map<UpdateUserTradeBotRequestDTO>(model);
                    await TradeBotClientService.UpdateTradingBotAsync(updateRequest, cancelToken);
                }
                else
                {
                    // Create New Trade Bot
                    var authState = await AuthenticationStateTask!;
                    var user = authState.User;
                    var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                    {
                        FormModel.UserId = userId;
                        var addRequest = Mapper.Map<AddUserTradeBotRequestDTO>(model);
                        await TradeBotClientService.AddUserTradingBotAsync(addRequest, cancelToken);
                    }
                }

                NavManager.NavigateTo("/trade-bots/list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving trade bot: {ex.Message}");
            }
        }

        private void Cancel()
        {
            NavManager.NavigateTo("/trade-bots/list");
        }

        private void PopulateDropdownMenusWithEnumerationValues()
        {
            MarketTypes = Enum.GetValues<TradeBotMarketType>()
                              .Cast<TradeBotMarketType>()
                              .Where(e => Convert.ToInt32(e) != 0) // Exclude 0
                              .Select(e => new DropDownItem<TradeBotMarketType> { Name = e.ToString(), Value = e })
                              .ToList();

            MarketDirections = Enum.GetValues<TradeBotMarketDirection>()
                                   .Cast<TradeBotMarketDirection>()
                                   .Where(e => Convert.ToInt32(e) != 0) // Exclude 0
                                   .Select(e => new DropDownItem<TradeBotMarketDirection> { Name = e.ToString(), Value = e })
                                   .ToList();

            ChannelStructures = Enum.GetValues<TradeBotChannelStructureType>()
                                    .Cast<TradeBotChannelStructureType>()
                                    .Where(e => Convert.ToInt32(e) != 0) // Exclude 0
                                    .Select(e => new DropDownItem<TradeBotChannelStructureType> { Name = e.ToString(), Value = e })
                                    .ToList();

            ChannelInfinities = Enum.GetValues<TradeBotChannelInfinityType>()
                                    .Cast<TradeBotChannelInfinityType>()
                                    .Where(e => Convert.ToInt32(e) != 0) // Exclude 0
                                    .Select(e => new DropDownItem<TradeBotChannelInfinityType> { Name = e.ToString(), Value = e })
                                    .ToList();

            TradeBotSlotTypes = Enum.GetValues<TradeBotSlotType>()
                                    .Cast<TradeBotSlotType>()
                                    .Where(e => Convert.ToInt32(e) != 0) // Exclude 0
                                    .Select(e => new DropDownItem<TradeBotSlotType> { Name = e.ToString(), Value = e })
                                    .ToList();

            QuantityTypes = Enum.GetValues<TradeBotQuantityType>()
                                .Cast<TradeBotQuantityType>()
                                .Where(e => Convert.ToInt32(e) != 0) // Exclude 0
                                .Select(e => new DropDownItem<TradeBotQuantityType> { Name = e.ToString(), Value = e })
                                .ToList();
        }

        public class DropDownItem<T>
        {
            public string Name { get; set; } = string.Empty;
            public T? Value { get; set; }
        }
    }
}
