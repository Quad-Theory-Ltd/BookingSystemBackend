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

        [HttpGet]
        public async Task<IActionResult> GetAllActiveScreen()
        {
            var screen = await _screenRepository.GetAllActiveScreenesAsync();
            return Ok(screen);
        }

        [HttpPost("CreateScreen")]
        public async Task<IActionResult> CreateScreen(ScreenView screen)
        {
            if(screen == null)
            {
                return BadRequest("Screen is Null");
            }

            await _screenRepository.CreateScreenAsync(screen);
            return NoContent();
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetScreen(string id)
        {
            var screen = await _screenRepository.GetScreenAsync(id);
            if (screen == null)
            {
                return NotFound("Screen not found.");
            }
            return Ok(screen);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreen(string id, [FromBody] ScreenView screen)
        {
            if (screen == null || screen.Id != id)
            {
                return BadRequest("Screen Id is Invalid!");
            }
            var existingScreen = await _screenRepository.GetScreenAsync(id);
            if (existingScreen == null)
            {
                return BadRequest(" Screen Not Found!");
            }
            await _screenRepository.UpdateScreenAsync(screen);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreen(string id)
        {
            var screen = await _screenRepository.GetScreenAsync(id);
            if (screen == null)
            {
                return NotFound("Screen not found.");
            }

            await _screenRepository.DeleteScreenAsync(id);
            return NoContent();
        }

        /* [HttpPost("CreateScreenFunction")]
         public async Task<IActionResult> CreateScreenFunction(CreateScreenFunctionView screenFunction)
         {
             if (screenFunction == null)
             {
                 return BadRequest("Screen is Null");
             }

             await _screenRepository.CreateScreenFunctionAsync(screenFunction);
             return NoContent();
         }*/
    }
}
