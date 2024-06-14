using BookingSundorbon.Features.Repositories.ParcelRepository;
using BookingSundorbon.Views.DTOs.ParcelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Parcel
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelRepository _parcelRepository;

        public ParcelController(IParcelRepository parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateParcel([FromBody] ParcelView parcel)
        {
            if (parcel == null)
            {
                return BadRequest("Parcel information is null.");
            }

            var newParcelId = await _parcelRepository.CreateParcelAsync(parcel);
            return CreatedAtAction(nameof(GetParcel), new { id = newParcelId }, newParcelId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParcel(long id)
        {
            var parcel = await _parcelRepository.GetParcelAsync(id);
            if (parcel == null)
            {
                return NotFound("Parcel information not found.");
            }
            return Ok(parcel);
        }
    }
}
