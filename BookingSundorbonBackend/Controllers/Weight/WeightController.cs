using BookingSundorbon.Features.Repositories.WeigthRepository;
using BookingSundorbon.Views.DTOs.WeightView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.WeightController
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly IWeightRepository _weightRepository;

        public WeightController(IWeightRepository weightRepository)
        {
            _weightRepository = weightRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeight([FromBody] CreateWeigthView weigth)
        {
            if(weigth == null)
            {
                return BadRequest("Weight is Null");
            }

            var weightId = await _weightRepository.CreateWeightAsync(weigth);

            return Ok(weightId);

        }

    }
}
