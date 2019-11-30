using CryptoCurrencyBrowser.Application.Cryptocurrencies.GetCryptocurrencyDetails;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Controllers
{
    [Route("api/[controller]")]
    public class CryptocurrencyController : Controller
    {
        private readonly IGetCryptocurrencyDetailsService _getCryptocurrencyDetailsService;


        public CryptocurrencyController(IGetCryptocurrencyDetailsService getCryptocurrencyDetailsService)
        {
            _getCryptocurrencyDetailsService = getCryptocurrencyDetailsService;
        }

        [Route("{id}")]
        public async Task<IActionResult> GetCryptocurrencyDetails(int id)
        {
            return Ok(await _getCryptocurrencyDetailsService.GetDetailsById(id));
        }
    }
}
