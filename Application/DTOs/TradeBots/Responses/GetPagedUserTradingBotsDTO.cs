namespace Application.DTOs.TradeBots.Responses
{
    public class GetPagedUserTradingBotsDTO
    {
        public IEnumerable<GetUserTradeBotResponseDTO> Bots { get; set; }
        public int TotalCount { get; set; }
    }
}
