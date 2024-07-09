using BookingSundorbon.Features.Repositories.ShippingServiceRepository;
using BookingSundorbon.Views.DTOs.ShippingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ShippingService
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingServiceController : ControllerBase
    {
        private readonly IShippingServiceRepository _shippingServiceRepository;

        public ShippingServiceController(IShippingServiceRepository shippingServiceRepository)
        {
            _shippingServiceRepository = shippingServiceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShippingService([FromBody] CreateShippingServiceView shippingService)
        {
            if(shippingService == null)
            {
                return BadRequest("Shipping Service is Null");
            }
            var shippingServiceId = await _shippingServiceRepository.CreateShippingServiceAsync(shippingService);

            return Ok(shippingServiceId);
        }

    }
}
