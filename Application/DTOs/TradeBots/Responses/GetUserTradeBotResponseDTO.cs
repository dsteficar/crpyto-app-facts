namespace Application.DTOs.TradeBots.Responses
{
    public class GetUserTradeBotResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
    }
}
