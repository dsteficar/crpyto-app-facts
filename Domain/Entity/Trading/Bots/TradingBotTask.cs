using Domain.Entity.Authentication;

namespace Domain.Entity.Trading.Bots
{
    public class TradingBotTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public virtual TradingBotSettings Settings { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
