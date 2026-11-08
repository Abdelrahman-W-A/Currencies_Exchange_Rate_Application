using Main_Project.Services.DTOs;

namespace Main_Project.Services.Contracts
{
    public interface IExchangeRateService
    {
        public Task<IReadOnlyList<FrankFurterExchangeRate>> GetCurrenciesExchangeRateAsync(string Base, CancellationToken ct = default);
    }
}
