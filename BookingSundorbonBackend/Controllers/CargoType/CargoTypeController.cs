using BookingSundorbon.Features.Repositories.CargoTypeRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.CargoType
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoTypeController : ControllerBase
    {
        private readonly ICargoTypeRepository _cargoTypeRepository;

        public CargoTypeController(ICargoTypeRepository cargoTypeRepository)
        {
            _cargoTypeRepository = cargoTypeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveCargoTypes()
        {
            var cities = await _cargoTypeRepository.GetAllActiveCargoTypesAsync();
            return Ok(cities);
        }
    }
}
