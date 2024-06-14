using BookingSundorbon.Features.Repositories.CityRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.CityController
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
        public async Task<IActionResult> GetAllCities()
        {
            var result = await _cityRepository.GetAllActiveCitiesAsync();
            if (result == null)
            {
                return NotFound("Cities not found.");
            }
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetSingleCity(int id, bool isActive)
        //{
        //    var result = await _cityRepository.GetCityByIdAsync(id, isActive);
        //    if (result == null)
        //    {
        //        return NotFound("City not found.");
        //    }
        //    return Ok(result);
        //}
    }
}
