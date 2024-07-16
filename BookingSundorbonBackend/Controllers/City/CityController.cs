using BookingSundorbon.Features.Repositories.CityRepository;
using BookingSundorbon.Views.DTOs.ActiveCityView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.City
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveCities()
        {
            var cities = await _cityRepository.GetAllActiveCitiesAsync();
            return Ok(cities);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] ActiveCityView city)
        {
            if (city == null)
            {
                return BadRequest("City is Null");
            }
            var cityId = await _cityRepository.CreateCityAsync(city);

            return CreatedAtAction(nameof(GetCity), new { id = cityId }, cityId);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetCity(int id)
        {
            var city = await _cityRepository.GetCityAsync(id);
            if (city == null)
            {
                return NotFound("City not found.");
            }
            return Ok(city);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] ActiveCityView city)
        {
            if (city == null || city.Id != id)
            {
                return BadRequest(" City Id is Invalid!");
            }
            var existingCity = await _cityRepository.GetCityAsync(id);
            if (existingCity == null)
            {
                return BadRequest(" City Not Found!");
            }
            await _cityRepository.UpdateCityAsync(city);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _cityRepository.GetCityAsync(id);
            if (city == null)
            {
                return NotFound("City not found.");
            }

            await _cityRepository.DeleteCityAsync(id);
            return NoContent();
        }
    }
}
