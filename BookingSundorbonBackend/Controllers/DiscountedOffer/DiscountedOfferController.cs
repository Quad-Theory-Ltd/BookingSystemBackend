using BookingSundorbon.Features.Repositories.DiscountedOfferRepository;
using BookingSundorbon.Views.DTOs.DiscountedOfferView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.DiscountedOffer
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountedOfferController : ControllerBase
    {

        private readonly IDiscountedOfferRepository _discountedOfferRepository;

        public DiscountedOfferController(IDiscountedOfferRepository discountedOfferRepository)
        {
            _discountedOfferRepository = discountedOfferRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveDiscountedOffers()
        {
            var discountedOffers = await _discountedOfferRepository.GetAllActiveDiscountedOffersAsync();
            return Ok(discountedOffers);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDiscountedOffer([FromBody] DiscountedOfferView discountedOffer)
        {
            if (discountedOffer == null)
            {
                return BadRequest("Discounted Offer is Null");
            }
            var discountedOfferId = await _discountedOfferRepository.CreateDiscountedOfferAsync(discountedOffer);

            return CreatedAtAction(nameof(GetDiscountedOffer), new { id = discountedOfferId }, discountedOfferId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDiscountedOffer(int id)
        {
            var discountedOffer = await _discountedOfferRepository.GetDiscountedOfferAsync(id);
            if (discountedOffer == null)
            {
                return NotFound("Discounted Offer not found.");
            }
            return Ok(discountedOffer);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscountedOffer(int id, [FromBody] DiscountedOfferView discountedOffer)
        {
            if (discountedOffer == null || discountedOffer.Id != id)
            {
                return BadRequest("Discounted Offer Id is Invalid!");
            }
            var existingDiscountedOffer = await _discountedOfferRepository.GetDiscountedOfferAsync(id);
            if (existingDiscountedOffer == null)
            {
                return BadRequest("DiscountedOffer Not Found!");
            }
            await _discountedOfferRepository.UpdateDiscountedOfferAsync(discountedOffer);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountedOffer(int id)
        {
            var discountedOffer = await _discountedOfferRepository.GetDiscountedOfferAsync(id);
            if (discountedOffer == null)
            {
                return NotFound("Discounted Offer not found.");
            }

            await _discountedOfferRepository.DeleteDiscountedOfferAsync(id);
            return NoContent();
        }

    }
}
