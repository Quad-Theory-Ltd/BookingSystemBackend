using BookingSundorbon.Features.Repositories.ParcelBookingInformationRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ParcelBookingInformation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelBookingInformationController : ControllerBase
    {
        private readonly IParcelBookingInformationRepository _parcelBookingInformationRepository;

        public ParcelBookingInformationController(IParcelBookingInformationRepository parcelBookingInformationRepository)
        {
            _parcelBookingInformationRepository = parcelBookingInformationRepository;
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetParcelInfoByUserId(string userId)
        {
            var count = await _parcelBookingInformationRepository.GetParcelInfoByUserIdAsync(userId);
            if (count == null)
            {
                return NotFound("Parcel Info not found.");
            }
            return Ok(count);
        }


        [HttpGet("Parcelcounts")]
        public async Task<IActionResult> GetParcelCounts()
        {
            var count = await _parcelBookingInformationRepository.GetParcelCounts();
            if (count == null)
            {
                return NotFound("Parcel Info not found.");
            }
            return Ok(count);
        }

        [HttpGet("Parcelcounts/dimensions")]
        public async Task<IActionResult> GetParcelCountsWithDimensions()
        {
            var count = await _parcelBookingInformationRepository.GetParcelCountsWithDimensions();
            if (count == null)
            {
                return NotFound("Parcel Info not found.");
            }
            return Ok(count);
        }
    }
}
