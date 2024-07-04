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


        [HttpGet]
        public async Task<ActionResult> GetTotalTransitionCost([FromQuery]GetTransitionCostView getTransitionCost)
        {
            try
            {
                if (getTransitionCost == null)
                {
                    return BadRequest();
                }
                var result = await _getTransitionCostRepository.GetTransitionCost(getTransitionCost);

                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateParcelBooking([FromBody] CreateParcelBookingView createParcelBookingView)
        {
            if (createParcelBookingView == null)
            {
                return BadRequest("Parcel Booking is null.");
            }

            var result = await _getTransitionCostRepository.CreateParcelBookingAsync(createParcelBookingView);
            return Ok(result);
        }
    }
}
