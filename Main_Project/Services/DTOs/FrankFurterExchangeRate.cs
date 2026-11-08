using System.Text.Json.Serialization;

namespace Main_Project.Services.DTOs
{
    public class FrankFurterExchangeRate
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = default!;

        [JsonPropertyName("base")]
        public string Base { get; set; } = default!;

        [JsonPropertyName("quote")]
        public string Quote { get; set; } = default!;

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
    }
}
