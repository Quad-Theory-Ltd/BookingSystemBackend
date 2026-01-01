using BookingSundorbon.Features.Repositories.QuotationRepository;
using BookingSundorbon.Views.DTOs.QuotationView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Quotation
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationRepository _quotationRepository;

        public QuotationController(IQuotationRepository quotationRepository)
        {
            _quotationRepository = quotationRepository;
        }

        [HttpPost("GetPricing")]
        public async Task<ActionResult<GetPricingResponseView>> GetPricing([FromBody] GetPricingView getPricingView)
        {
            try
            {
                if (getPricingView == null)
                {
                    return BadRequest(new GetPricingResponseView
                    {
                        IsSuccess = 0,
                        Message = "Request data is required"
                    });
                }

                var result = await _quotationRepository.GetPricingAsync(getPricingView);
                
                if (result.IsSuccess == 1)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GetPricingResponseView
                {
                    IsSuccess = 0,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }
    }
}

