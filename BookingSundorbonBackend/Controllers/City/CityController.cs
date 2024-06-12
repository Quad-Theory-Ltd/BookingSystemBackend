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
    }
}
