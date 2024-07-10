using BookingSundorbon.Features.Repositories.WeigthRepository;
using BookingSundorbon.Views.DTOs.WeightView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.WeightController
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly IWeightRepository _weightRepository;

        public WeightController(IWeightRepository weightRepository)
        {
            _weightRepository = weightRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveWeights()
        {
            var weights = await _weightRepository.GetAllActiveWeightsAsync();
            return Ok(weights);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeight([FromBody] WeightView weigth)
        {
            if(weigth == null)
            {
                return BadRequest("Weight is Null");
            }

            var weightId = await _weightRepository.CreateWeightAsync(weigth);

            return CreatedAtAction(nameof(GetWeight), new {id = weightId}, weightId);

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetWeight(int id)
        {
            var weight = await _weightRepository.GetWeightAsync(id);
            if (weight == null)
            {
                return NotFound("Weight not found.");
            }
            return Ok(weight);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeight(int id, [FromBody] WeightView weightType)
        {
            if (weightType == null || weightType.Id != id)
            {
                return BadRequest("Weight Data is Invalid!");
            }
            var existingWeight = await _weightRepository.GetWeightAsync(id);
            if (existingWeight == null)
            {
                return BadRequest(" Weight Not Found!");
            }
            await _weightRepository.UpdateWeightAsync(weightType);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeight(int id)
        {
            var weight = await _weightRepository.GetWeightAsync(id);
            if (weight == null)
            {
                return NotFound(" Weight not found.");
            }

            await _weightRepository.DeleteWeightAsync(id);
            return NoContent();
        }

    }
}
