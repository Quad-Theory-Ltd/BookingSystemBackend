using BookingSundorbon.Features.Repositories.ParcelDetailsRepository;
using BookingSundorbon.Views.DTOs.ParcelDetailsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ParcelDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelDetailsController : ControllerBase
    {

        private readonly IParcelDetailsRepository _parcelDetailsRepository;

        public ParcelDetailsController(IParcelDetailsRepository parcelDetailsRepository)
        {
            _parcelDetailsRepository = parcelDetailsRepository;
        }




        [HttpGet("GetParcelDetailsByParcelNo/{parcelNo}")]

        public async Task<IActionResult> GetParcelDetailsByParcelNo(int parcelNo)
        {
            var parcelDetails = await _parcelDetailsRepository.GetParcelDetailsByParcelNoAsync(parcelNo);
            if (parcelDetails == null)
            {
                return NotFound("Parcel Details not found.");
            }
            return Ok(parcelDetails);
        }



    }
}
