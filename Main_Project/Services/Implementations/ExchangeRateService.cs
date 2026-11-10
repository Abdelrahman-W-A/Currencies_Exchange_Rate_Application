using Main_Project.Services.Contracts;
using Main_Project.Services.DTOs;
using System.Text.Json;

namespace Main_Project.Services.Implementations
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        public ExchangeRateService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IReadOnlyList<FrankFurterExchangeRate>> GetCurrenciesExchangeRateAsync(string Base, CancellationToken ct = default)
        {
            if (Base == null)
                return [];

            string[] quotes =
            {
                "USD", "EUR", "GBP", "JPY", "CHF",
                "CNY", "CAD", "AUD", "NZD", "HKD",
                "SGD", "INR", "KRW", "SEK", "MXN",
                "SAR", "AED", "EGP", "KWD", "QAR"
            };

            var quotesString = string.Join(",", quotes);

            var url = $"https://api.frankfurter.dev/v2/rates?base={Base}&quotes={quotesString}";

            var exchangeRates = await _httpClient.GetFromJsonAsync<IReadOnlyList<FrankFurterExchangeRate>>(url,ct); // return 20 currencies exchange rates

            if (exchangeRates == null)
                return [];

            return exchangeRates;
        }

        public async Task<IReadOnlyList<PreciousMetal>> GetPreciousMetalsAsync(CancellationToken ct = default)
        {
            string url = "https://www.kitco.com/_next/data/hnK7mVhxzatyoGyFLTkVx/price/base-metals.json";

            var respone = await _httpClient.GetAsync(url, ct);

            respone.EnsureSuccessStatusCode();

            var json = await respone.Content.ReadAsStringAsync(ct);

            var jsonDocument = JsonDocument.Parse(json);

            var root = jsonDocument.RootElement;

            var queries = root.GetProperty("pageProps")
                .GetProperty("dehydratedState")
                .GetProperty("queries");
            
            var data = queries[0]
                .GetProperty("state")
                .GetProperty("data");

            var gold = data.GetProperty("gold").GetProperty("results")[0];
            var goldProperties = GetPreciousMetalProperties(gold, "Gold", "AU");

            var silver = data.GetProperty("silver").GetProperty("results")[0];
            var silverProperties = GetPreciousMetalProperties(silver, "Silver", "AG");

            var platinum = data.GetProperty("platinum").GetProperty("results")[0];
            var platinumProperties = GetPreciousMetalProperties(platinum, "Platinum", "PT");

            var palladium = data.GetProperty("palladium").GetProperty("results")[0];
            var palladiumProperties = GetPreciousMetalProperties(palladium, "Palladium", "PD");

            var rhodium = data.GetProperty("rhodium").GetProperty("results")[0];
            var rhodiumProperties = GetPreciousMetalProperties(rhodium, "Rhodium", "RH");

            return new List<PreciousMetal> { goldProperties, silverProperties, platinumProperties, palladiumProperties, rhodiumProperties };
        }

        public PreciousMetal GetPreciousMetalProperties(JsonElement preciousMetal, string name, string symbol)
        {
            var preciousMetalProperties = new PreciousMetal
            {
                Name = name,
                Symbol = symbol,
                ask = preciousMetal.GetProperty("ask").ToString(),
                bid = preciousMetal.GetProperty("bid").ToString(),
                mid = preciousMetal.GetProperty("mid").ToString(),
                change = preciousMetal.GetProperty("change").ToString(),
                changePercentage = preciousMetal.GetProperty("changePercentage").ToString(),
                high = preciousMetal.GetProperty("high").ToString(),
                low = preciousMetal.GetProperty("low").ToString(),
                unit = preciousMetal.GetProperty("unit").ToString()
            };
            return preciousMetalProperties;
        }

    }
}
