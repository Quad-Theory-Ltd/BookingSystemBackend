using BookingSundorbon.Features.Repositories.PickupRepository;
using BookingSundorbon.Views.DTOs.PickupView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Pickup
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupController : ControllerBase
    {
        private readonly IPickupRepository _pickupRepository;

        public PickupController(IPickupRepository pickupRepository)
        {
            _pickupRepository = pickupRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePickUp([FromBody] PickupView pickup)
        {
            if(pickup == null)
            {
                return BadRequest("Pickup is null.");
            }

            var pickupId = await _pickupRepository.CreatePickupAsync(pickup);
            return CreatedAtAction(nameof(GetPickup), new {id = pickupId}, pickupId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPickup(int id)
        {
            var pickup = await _pickupRepository.GetPickupAsync(id);
            if (pickup == null)
            {
                return NotFound("Pick up not found.");
            }
            return Ok(pickup);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivePickups()
        {
            var pickup = await _pickupRepository.GetAllActivePickupUnitsAsync();
            return Ok(pickup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePickup(int id, [FromBody] PickupView pickup)
        {
            if (pickup == null || pickup.Id != id)
            {
                return BadRequest("Pickup Data is Invalid!");
            }
            var existingPickup = await _pickupRepository.GetPickupAsync(id);
            if (existingPickup == null)
            {
                return BadRequest("Pickup data Not Found!");
            }
            await _pickupRepository.UpdatePickupAsync(pickup);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePickup(int id)
        {
            var pickup = await _pickupRepository.GetPickupAsync(id);
            if (pickup == null)
            {
                return NotFound("Pickup data not found.");
            }

            await _pickupRepository.DeletePickupAsync(id);
            return NoContent();
        }
    }
}
