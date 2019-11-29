using CryptoCurrencyBrowser.Application.Cryptocurrencies.GetCryptocurrencyCards;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencyBrowser.Controllers
{
    [Route("api/[controller]")]
    public class CryptocurrenciesController : Controller
    {
        private readonly IGetCryptocurrencyCardsService _getCryptocurrencyCardsService;

        public CryptocurrenciesController(IGetCryptocurrencyCardsService getCryptocurrencyCardsService)
        {
            _getCryptocurrencyCardsService = getCryptocurrencyCardsService;
        }
    }
}
