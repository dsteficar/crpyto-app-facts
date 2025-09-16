using System.Text.Json.Serialization;

namespace Domain.ResultObjects.Binance
{
    public class BinanceExchangeInfoResult
    {
        [JsonPropertyName("symbols")]
        public List<SymbolResult> Symbols { get; set; } = new List<SymbolResult>();
    }

    public class SymbolResult
    {
        [JsonPropertyName("symbol")] // JSON property name
        public string SymbolName { get; set; } = string.Empty;

        [JsonPropertyName("contractType")] // JSON property name
        public string ContractType { get; set; } = string.Empty;
    }
}
