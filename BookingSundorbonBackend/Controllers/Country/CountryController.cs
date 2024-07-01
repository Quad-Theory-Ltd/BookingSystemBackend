
using BookingSundorbon.Features.Repositories.CountryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Country
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveCountries()
        {
            var countries = await _countryRepository.GetAllActiveCountriesAsync();
            return Ok(countries);
        }
    }
}
