using BookingSundorbon.Features.Repositories.SenderDetailsRepository;
using BookingSundorbon.Views.DTOs.SenderDetailsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.SenderDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderDetailsController : ControllerBase
    {

        private readonly ISenderDetailsRepository _deviceRepository;

        public SenderDetailsController(ISenderDetailsRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet("GetPickupAndDeliveryPoint/{parcelOrderId}")]

        public async Task<IActionResult> GetPickupAndDeliveryPoint(int parcelOrderId)
        {
            var point = await _deviceRepository.GetPickupAndDeliveryPointAsync(parcelOrderId);
            if (point == null)
            {
                return NotFound("Not found.");
            }
            return Ok(point);
        }



    }
}
