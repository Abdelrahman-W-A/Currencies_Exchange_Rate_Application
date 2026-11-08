using Main_Project.Services.Contracts;
using Main_Project.Services.DTOs;

namespace Main_Project.Services.Implementations
{
    public class ExchangeRateService : IExchangeRateService
    {
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

            var exchangeRates = await new HttpClient().GetFromJsonAsync<IReadOnlyList<FrankFurterExchangeRate>>(url,ct); // return 20 currencies exchange rates

            if (exchangeRates == null)
                return [];

            return exchangeRates;
        }
    }
}
