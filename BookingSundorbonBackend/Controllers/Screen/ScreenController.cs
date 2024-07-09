using BookingSundorbon.Features.Repositories.ScreenRepository;
using BookingSundorbon.Views.DTOs.ScreenView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Screen
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : ControllerBase
    {
        private readonly IScreenRepository _screenRepository;

        public ScreenController(IScreenRepository screenRepository)
        {
            _screenRepository = screenRepository;
        }

        [HttpPost("CreateScreen")]
        public async Task<IActionResult> CreateScreen(CreateScreenView screen)
        {
            if(screen == null)
            {
                return BadRequest("Screen is Null");
            }

            await _screenRepository.CreateScreenAsync(screen);
            return NoContent();
        }

        [HttpPost("CreateScreenFunction")]
        public async Task<IActionResult> CreateScreenFunction(CreateScreenFunctionView screenFunction)
        {
            if (screenFunction == null)
            {
                return BadRequest("Screen is Null");
            }

            await _screenRepository.CreateScreenFunctionAsync(screenFunction);
            return NoContent();
        }
    }
}
