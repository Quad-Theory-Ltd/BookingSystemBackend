using BookingSundorbon.Features.Repositories.AdditionalCostRepository;
using BookingSundorbon.Views.DTOs.AdditionalCostView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.AdditionalCost
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalCostController : ControllerBase
    {

        private readonly IAdditionalCostRepository _additionalCostRepository;

        public AdditionalCostController(IAdditionalCostRepository additionalCostRepository)
        {
            _additionalCostRepository = additionalCostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveAdditionalCost()
        {
            var additionalCost = await _additionalCostRepository.GetAllActiveAdditionalCostAsync();
            return Ok(additionalCost);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAdditionalCost([FromBody] AdditionalCostView additionalCost)
        {
            if (additionalCost == null)
            {
                return BadRequest("Additional cost is Null");
            }
            var additionalCostId = await _additionalCostRepository.CreateAdditionalCostAsync(additionalCost);

            return CreatedAtAction(nameof(GetAdditionalCost), new { id = additionalCostId }, additionalCostId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAdditionalCost(int id)
        {
            var additionalCost = await _additionalCostRepository.GetAdditionalCostAsync(id);
            if (additionalCost == null)
            {
                return NotFound("Additional Cost not found.");
            }
            return Ok(additionalCost);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdditionalCost(int id, [FromBody] AdditionalCostView additionalCost)
        {
            if (additionalCost == null || additionalCost.Id != id)
            {
                return BadRequest(" Additional Cost Id is Invalid!");
            }
            var existingAdditionalCost = await _additionalCostRepository.GetAdditionalCostAsync(id);
            if (existingAdditionalCost == null)
            {
                return BadRequest(" Additional Cost Not Found!");
            }
            await _additionalCostRepository.UpdateAdditionalCostAsync(additionalCost);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalCost(int id)
        {
            var additionalCost = await _additionalCostRepository.GetAdditionalCostAsync(id);
            if (additionalCost == null)
            {
                return NotFound(" Additional Cost not found.");
            }

            await _additionalCostRepository.DeleteAdditionalCostAsync(id);
            return NoContent();
        }

    }
}
