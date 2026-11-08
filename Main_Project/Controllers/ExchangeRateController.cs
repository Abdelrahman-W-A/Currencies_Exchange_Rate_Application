using Main_Project.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Main_Project.Controllers
{
    public class ExchangeRateController(IExchangeRateService exchangeRateService) : Controller
    {
        private readonly IExchangeRateService _exchangeRateService = exchangeRateService;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string Base = "USD", CancellationToken ct = default)
        {
            var exchangeRates = await _exchangeRateService.GetCurrenciesExchangeRateAsync(Base, ct);
            return View(exchangeRates);
        }

    }
}
