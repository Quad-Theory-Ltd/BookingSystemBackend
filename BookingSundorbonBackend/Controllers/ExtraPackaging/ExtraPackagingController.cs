using BookingSundorbon.Features.Repositories.ExtraPackagingRepository;
using BookingSundorbon.Views.DTOs.ExtraPackagingView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ExtraPackaging
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraPackagingController : ControllerBase
    {

        private readonly IExtraPackagingRepository _extraPackagingRepository;

        public ExtraPackagingController(IExtraPackagingRepository extraPackagingRepository)
        {
            _extraPackagingRepository = extraPackagingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveExtraPackagings()
        {
            var extraPackaging = await _extraPackagingRepository.GetAllActiveExtraPackagingsAsync();
            return Ok(extraPackaging);
        }


        [HttpPost]
        public async Task<IActionResult> CreateExtraPackaging([FromBody] ExtraPackagingView extraPackaging)
        {
            if (extraPackaging == null)
            {
                return BadRequest("Extra Packaging is Null");
            }
            var extraPackagingId = await _extraPackagingRepository.CreateExtraPackagingAsync(extraPackaging);

            return CreatedAtAction(nameof(GetExtraPackaging), new { id = extraPackagingId }, extraPackagingId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetExtraPackaging(int id)
        {
            var extraPackaging = await _extraPackagingRepository.GetExtraPackagingAsync(id);
            if (extraPackaging == null)
            {
                return NotFound("Extra Packaging not found.");
            }
            return Ok(extraPackaging);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExtraPackaging(int id, [FromBody] ExtraPackagingView extraPackaging)
        {
            if (extraPackaging == null || extraPackaging.Id != id)
            {
                return BadRequest("Extra Packaging Id is Invalid!");
            }
            var existingExtraPackaging = await _extraPackagingRepository.GetExtraPackagingAsync(id);
            if (existingExtraPackaging == null)
            {
                return BadRequest("Extra Packaging Not Found!");
            }
            await _extraPackagingRepository.UpdateExtraPackagingAsync(extraPackaging);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExtraPackaging(int id)
        {
            var extraPackaging = await _extraPackagingRepository.GetExtraPackagingAsync(id);
            if (extraPackaging == null)
            {
                return NotFound("Extra Packaging not found.");
            }

            await _extraPackagingRepository.DeleteExtraPackagingAsync(id);
            return NoContent();
        }

    }
}
