using Domain.Enums;

namespace Domain.Entity.Trading.Bots
{
    public class TradingBotSettings
    {
        public int Id { get; set; }
        public int TradingBotTaskId { get; set; }
        public TradeBotStatus BotStatus { get; set; } = TradeBotStatus.Initialised;
        public TradeBotMarketType MarketType { get; set; }
        public TradeBotMarketDirection MarketDirection { get; set; }
        public TradeBotChannelStructureType ChannelStructureType { get; set; }
        // Horizontalni ili kosi kanal
        public TradeBotChannelInfinityType ChannelInfinityType { get; set; }
        public string BaseAsset { get; set; } = string.Empty;
        public string QuoteAsset { get; set; } = string.Empty;
        public int PricePrecision { get; set; }
        // Broj decimala za cijenu
        public int QuantityPrecision { get; set; }
        // Broj decimala za količinu
        public TradeBotSlotType SlotType { get; set; } // može biti fiksni ili u postotku
        public decimal SlotSize { get; set; }
        // U apsolutnom iznosu ako je SlotType = Fixed, a u postotnom iznosu ako je SlotType = Percentage > 0.01 znači 1%
        public decimal StartingChannelPrice { get; set; }
        // Ovo je donja granica kanala, ako se radi o spot marketu ili long futures marketu, a gornja granica kanala ako se radi o short futures marketu
        public decimal? EndingChannelPrice { get; set; }
        // Ova cijena ne mora biti definirana jer se sve računa iz početne cijene i broja slotova
        public decimal StopLossPrice { get; set; }
        // Ako je ova cijena = null, onda nije definiran StopLoss. Nije preporučljivo, ali moguće
        public decimal TriggerPrice { get; set; }
        public TradeBotQuantityType QuantityType { get; set; }
        public decimal QuantityPerSlot { get; set; }
        public int NumberOfSlots { get; set; }
        public int OrdersCount { get; set; } = 0;

        public virtual TradingBotTask TradingBotTask { get; set; }
    }
}
