using BookingSundorbon.Features.Repositories.DiscountedOfferDetailRepository;
using BookingSundorbon.Views.DTOs.DiscountedOfferDetailView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.DiscountedOfferDetail
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountedOfferDetailController : ControllerBase
    {

        private readonly IDiscountedOfferDetailRepository _discountedOfferDetailRepository;

        public DiscountedOfferDetailController(IDiscountedOfferDetailRepository discountedOfferDetailRepository)
        {
            _discountedOfferDetailRepository = discountedOfferDetailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscountedOfferDetails()
        {
            var discountedOfferDetails = await _discountedOfferDetailRepository.GetAllDiscountedOfferDetailsAsync();
            return Ok(discountedOfferDetails);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDiscountedOfferDetail([FromBody] DiscountedOfferDetailView discountedOfferDetail)
        {
            if (discountedOfferDetail == null)
            {
                return BadRequest("Discounted Offer Details is Null");
            }
            var discountedOfferDetailId = await _discountedOfferDetailRepository.CreateDiscountedOfferDetailAsync(discountedOfferDetail);

            return CreatedAtAction(nameof(GetDiscountedOfferDetail), new { id = discountedOfferDetailId }, discountedOfferDetailId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDiscountedOfferDetail(int id)
        {
            var discountedOfferDetail = await _discountedOfferDetailRepository.GetDiscountedOfferDetailAsync(id);
            if (discountedOfferDetail == null)
            {
                return NotFound("Discounted Offer Details not found.");
            }
            return Ok(discountedOfferDetail);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscountedOfferDetail(int id, [FromBody] DiscountedOfferDetailView discountedOfferDetail)
        {
            if (discountedOfferDetail == null || discountedOfferDetail.Id != id)
            {
                return BadRequest("Discounted Offer Details Id is Invalid!");
            }
            var existingDiscountedOfferDetail = await _discountedOfferDetailRepository.GetDiscountedOfferDetailAsync(id);
            if (existingDiscountedOfferDetail == null)
            {
                return BadRequest("Discounted Offer Details Not Found!");
            }
            await _discountedOfferDetailRepository.UpdateDiscountedOfferDetailAsync(discountedOfferDetail);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountedOfferDetail(int id)
        {
            var discountedOfferDetail = await _discountedOfferDetailRepository.GetDiscountedOfferDetailAsync(id);
            if (discountedOfferDetail == null)
            {
                return NotFound("Discounted Offer Details not found.");
            }

            await _discountedOfferDetailRepository.DeleteDiscountedOfferDetailAsync(id);
            return NoContent();
        }

    }
}
