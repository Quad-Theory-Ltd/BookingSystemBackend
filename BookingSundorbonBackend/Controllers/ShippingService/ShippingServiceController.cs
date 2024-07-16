using BookingSundorbon.Features.Repositories.ShippingServiceRepository;
using BookingSundorbon.Views.DTOs.ShippingServiceView;
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

        [HttpGet]
        public async Task<IActionResult> GetAllActiveShippingService()
        {
            var shippingservice = await _shippingServiceRepository.GetAllActiveShippingServiceesAsync();
            return Ok(shippingservice);
        }


        [HttpPost]
        public async Task<IActionResult> CreateShippingService([FromBody] ShippingServiceView shippingService)
        {
            if(shippingService == null)
            {
                return BadRequest("Shipping Service is Null");
            }
            var shippingServiceId = await _shippingServiceRepository.CreateShippingServiceAsync(shippingService);

            return CreatedAtAction(nameof(GetShippingService), new {id = shippingServiceId}, shippingServiceId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetShippingService(int id)
        {
            var shippingService = await _shippingServiceRepository.GetShippingServiceAsync(id);
            if (shippingService == null)
            {
                return NotFound("Shipping Service not found.");
            }
            return Ok(shippingService);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShippingService(int id, [FromBody] ShippingServiceView shippingService)
        {
            if (shippingService == null || shippingService.Id != id)
            {
                return BadRequest("Shipping Service Id is Invalid!");
            }
            var existingShippingService = await _shippingServiceRepository.GetShippingServiceAsync(id);
            if (existingShippingService == null)
            {
                return BadRequest("Shipping Service Not Found!");
            }
            await _shippingServiceRepository.UpdateShippingServiceAsync(shippingService);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingService(int id)
        {
            var shippingService = await _shippingServiceRepository.GetShippingServiceAsync(id);
            if (shippingService == null)
            {
                return NotFound("Shipping Service not found.");
            }

            await _shippingServiceRepository.DeleteShippingServiceAsync(id);
            return NoContent();
        }


    }
}
