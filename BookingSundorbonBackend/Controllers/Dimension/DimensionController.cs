using BookingSundorbon.Features.Repositories.DimensionRepository;
using BookingSundorbon.Views.DTOs.DimensionView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Dimension
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimensionController : ControllerBase
    {

        private readonly IDimensionRepository _dimensionRepository;

        public DimensionController(IDimensionRepository dimensionRepository)
        {
            _dimensionRepository = dimensionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveDimensions()
        {
            var dimensions = await _dimensionRepository.GetAllActiveDimensionsAsync();
            return Ok(dimensions);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDimension([FromBody] DimensionView dimension)
        {
            if (dimension == null)
            {
                return BadRequest("Dimension is Null");
            }
            var dimensionId = await _dimensionRepository.CreateDimensionAsync(dimension);

            return CreatedAtAction(nameof(GetDimension), new { id = dimensionId }, dimensionId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDimension(int id)
        {
            var dimension = await _dimensionRepository.GetDimensionAsync(id);
            if (dimension == null)
            {
                return NotFound("Dimension not found.");
            }
            return Ok(dimension);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDimension(int id, [FromBody] DimensionView dimension)
        {
            if (dimension == null || dimension.Id != id)
            {
                return BadRequest(" Dimension Id is Invalid!");
            }
            var existingDimension = await _dimensionRepository.GetDimensionAsync(id);
            if (existingDimension == null)
            {
                return BadRequest(" Dimension Not Found!");
            }
            await _dimensionRepository.UpdateDimensionAsync(dimension);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimension(int id)
        {
            var dimension = await _dimensionRepository.GetDimensionAsync(id);
            if (dimension == null)
            {
                return NotFound("Dimension not found.");
            }

            await _dimensionRepository.DeleteDimensionAsync(id);
            return NoContent();
        }

        [HttpGet("GetDimensionPriceById/{id}")]

        public async Task<IActionResult> GetDimensionPriceById(int id)
        {
            var dimension = await _dimensionRepository.GetDimensionPriceByIdAsync(id);
            if (dimension == null)
            {
                return NotFound("Dimension not found.");
            }
            return Ok(dimension);
        }
    }
}
