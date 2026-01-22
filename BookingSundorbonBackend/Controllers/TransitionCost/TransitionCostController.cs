using BookingSundorbon.Features.Repositories.BranchRepository;
using BookingSundorbon.Features.Repositories.CompanyRepository;
using BookingSundorbon.Features.Repositories.GetTransitionCostRepository;
using BookingSundorbon.Views.DTOs.CompanyView;
using BookingSundorbon.Views.DTOs.GetTransitionCostView;
using BookingSundorbon.Views.DTOs.TransitionCostView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.GetTransitionCost
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransitionCostController : ControllerBase
    {
        private readonly ITransitionCostRepository _getTransitionCostRepository;

        public TransitionCostController (ITransitionCostRepository getTransitionCostRepository)
        {
            _getTransitionCostRepository = getTransitionCostRepository;
        }


        [HttpPost]
        [Route("GetTotalTransitionCost")]
        public async Task<IActionResult> GetTotalTransitionCost([FromBody] List<GetTransitionCostView> getTransitionCost)
        {
            try
            {
                if (getTransitionCost == null || !getTransitionCost.Any())
                    return BadRequest("Request list is empty!");

                var result = await _getTransitionCostRepository.GetTransitionCost(getTransitionCost);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateParcelBooking([FromBody] List<CreateParcelBookingView> createParcelBookingViews)
        {
            if (createParcelBookingViews == null)
            {
                return BadRequest("Parcel Booking is null.");
            }

            var result = await _getTransitionCostRepository.CreateParcelBookingAsync(createParcelBookingViews);
            return Ok(result);
        }
        
    }
}
