using BookingSundorbon.Features.Repositories.IssueRepository;
using BookingSundorbon.Features.Repositories.SenderDetailsRepository;
using BookingSundorbon.Views.DTOs.SenderDetails;
using BookingSundorbon.Views.DTOs.SenderDetailsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.SenderDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderDetailsController : ControllerBase
    {

        private readonly ISenderDetailsRepository _senderDetailsRepository;

        public SenderDetailsController(ISenderDetailsRepository senderDetailsRepository)
        {
            _senderDetailsRepository = senderDetailsRepository;
        }

        [HttpPost("GetPickupAndDeliveryPoint")]

        public async Task<IActionResult> GetPickupAndDeliveryPoint([FromBody] List <int> parcelOrderIds)
        {

            var allPoints = new List<PickUpAndDeliveryInfoView>();

            foreach(var parcelOrderId in parcelOrderIds)
            {
                var point = await _senderDetailsRepository.GetPickupAndDeliveryPointAsync(parcelOrderId);
                if (point == null)
                {
                    return NotFound("Not found.");
                }
                allPoints.Add(point);
            }

           
            return Ok(allPoints);
        }

        [HttpGet("GetAllParcelNo")]

        public async Task<IActionResult> GetAllParcelOrderNo()
        {
            var parcelNo = await _senderDetailsRepository.GetAllParcelOrderNo();
            if (parcelNo == null)
            {
                return NotFound("Parcel Order  No not found.");
            }
            return Ok(parcelNo);
        }


    }
}
