using BookingSundorbon.Features.Repositories.CargoTypeRepository;
using BookingSundorbon.Views.DTOs.CargoTypeView;
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


        [HttpPost]
        public async Task<IActionResult> CreateCargoType([FromBody] ActiveCargoTypeView cargoType)
        {
            if (cargoType == null)
            {
                return BadRequest("Cargo Type is Null");
            }
            var cargoTypeId = await _cargoTypeRepository.CreateCargoTypeAsync(cargoType);

            return CreatedAtAction(nameof(GetCargoType), new { id = cargoTypeId }, cargoTypeId);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetCargoType(int id)
        {
            var cargoType = await _cargoTypeRepository.GetCargoTypeAsync(id);
            if (cargoType == null)
            {
                return NotFound("Cargo Type not found.");
            }
            return Ok(cargoType);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCargoType(int id, [FromBody] ActiveCargoTypeView cargoType)
        {
            if (cargoType == null || cargoType.Id != id)
            {
                return BadRequest("Cargo Type Id is Invalid!");
            }
            var existingCargoType = await _cargoTypeRepository.GetCargoTypeAsync(id);
            if (existingCargoType == null)
            {
                return BadRequest(" Cargo Type Not Found!");
            }
            await _cargoTypeRepository.UpdateCargoTypeAsync(cargoType);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargoType(int id)
        {
            var cargoType = await _cargoTypeRepository.GetCargoTypeAsync(id);
            if (cargoType == null)
            {
                return NotFound(" Cargo Type not found.");
            }

            await _cargoTypeRepository.DeleteCargoTypeAsync(id);
            return NoContent();
        }
    }
}
