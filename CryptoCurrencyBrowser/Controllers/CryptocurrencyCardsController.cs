using CryptoCurrencyBrowser.Application.Cryptocurrencies.GetCryptocurrencyCards;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptocurrencyCardsController : Controller
    {
        private readonly IGetCryptocurrencyCardsService _getCryptocurrencyCardsService;

        public CryptocurrencyCardsController(IGetCryptocurrencyCardsService getCryptocurrencyCardsService)
        {
            _getCryptocurrencyCardsService = getCryptocurrencyCardsService;
        }

        [Route("{pageNumber}")]
        public async Task<IActionResult> GetCards(int pageNumber)
        {
            return Ok(await _getCryptocurrencyCardsService.GetCryptocurrencyCards(pageNumber));
        }
    }
}
