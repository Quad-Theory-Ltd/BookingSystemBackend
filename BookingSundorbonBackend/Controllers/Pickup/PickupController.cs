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
        public async Task<IActionResult> CreatePickUp([FromBody] CreatePickupView pickup)
        {
            if(pickup == null)
            {
                return BadRequest();
            }

            var pickupId = await _pickupRepository.CreatePickupAsync(pickup);
            return Ok(pickupId);
        }
    }
}
