using BookingSundorbon.Features.Repositories.CurrencyRepository;
using BookingSundorbon.Views.DTOs.CurrencyView;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Currency
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        [HttpGet("GetAllActiveCurrency")]
       public async Task<IActionResult> GetAllActiveCurrencyAsync()
        {
            var currency= await _currencyRepository.GetAllActiveCurrencyAsync();
            return Ok(currency); 
        }

    }
}
