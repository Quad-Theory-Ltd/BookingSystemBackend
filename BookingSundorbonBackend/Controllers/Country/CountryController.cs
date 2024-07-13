using BookingSundorbon.Features.Repositories.CountryRepository;
using BookingSundorbon.Views.DTOs.CountryView;
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


        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] ActiveCountryView country)
        {
            if (country == null)
            {
                return BadRequest("Country is Null");
            }
            var countryId = await _countryRepository.CreateCountryAsync(country);

            return CreatedAtAction(nameof(GetCountry), new { id = countryId }, countryId);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _countryRepository.GetCountryAsync(id);
            if (country == null)
            {
                return NotFound("Country not found.");
            }
            return Ok(country);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] ActiveCountryView country)
        {
            if (country == null || country.Id != id)
            {
                return BadRequest(" Country Id is Invalid!");
            }
            var existingCountry = await _countryRepository.GetCountryAsync(id);
            if (existingCountry == null)
            {
                return BadRequest(" Country Not Found!");
            }
            await _countryRepository.UpdateCountryAsync(country);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepository.GetCountryAsync(id);
            if (country == null)
            {
                return NotFound("Country not found.");
            }

            await _countryRepository.DeleteCountryAsync(id);
            return NoContent();
        }
    }
}
