using Domain.Enums;

namespace WebApplicationUI.Models.Trading.Bots
{
    public class TradingBotEditFormModel
    {
        // TradeBotTask properties
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }

        // TradeBotSettings properties
        public TradeBotStatus BotStatus { get; set; } = TradeBotStatus.Initialised;
        public TradeBotMarketType MarketType { get; set; }
        public TradeBotMarketDirection MarketDirection { get; set; }
        public TradeBotChannelStructureType ChannelStructureType { get; set; }
        public TradeBotChannelInfinityType ChannelInfinityType { get; set; }
        public string BaseAsset { get; set; } = string.Empty;
        public string QuoteAsset { get; set; } = string.Empty;
        public int PricePrecision { get; set; }
        public int QuantityPrecision { get; set; }
        public TradeBotSlotType SlotType { get; set; }
        public decimal SlotSize { get; set; }
        public decimal StartingChannelPrice { get; set; }
        public decimal? EndingChannelPrice { get; set; }
        public decimal StopLossPrice { get; set; }
        public decimal TriggerPrice { get; set; }
        public TradeBotQuantityType QuantityType { get; set; }
        public decimal QuantityPerSlot { get; set; }
        public int NumberOfSlots { get; set; }
    }
}
