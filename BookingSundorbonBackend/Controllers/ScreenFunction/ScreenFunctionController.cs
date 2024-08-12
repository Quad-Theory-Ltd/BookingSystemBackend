using BookingSundorbon.Features.Repositories.ScreenFunctionRepository;
using BookingSundorbon.Views.DTOs.ScreenFunctionView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Screen
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenFunctionController : ControllerBase
    {
        private readonly IScreenFunctionRepository _screenFunctionRepository;

        public ScreenFunctionController(IScreenFunctionRepository screenFunctionRepository)
        {
            _screenFunctionRepository = screenFunctionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveScreenFunction()
        {
            var screen = await _screenFunctionRepository.GetAllActiveScreenesFunctionAsync();
            return Ok(screen);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScreen(ScreenFunctionView screenFunction)
        {
            if(screenFunction == null)
            {
                return BadRequest("Screen Function is Null");
            }

            await _screenFunctionRepository.CreateScreenFunctionAsync(screenFunction);
            return Created("", "Created");
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetScreen(string id)
        {
            var screenFunction = await _screenFunctionRepository.GetScreenFunctionAsync(id);
            if (screenFunction == null)
            {
                return NotFound("Screen Function not found.");
            }
            return Ok(screenFunction);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreenFunction(string id, [FromBody] ScreenFunctionView screenFunction)
        {
            if (screenFunction == null || screenFunction.Id != id)
            {
                return BadRequest("Screen Function Id is Invalid!");
            }
            var existingScreen = await _screenFunctionRepository.GetScreenFunctionAsync(id);
            if (existingScreen == null)
            {
                return BadRequest(" Screen Function Not Found!");
            }
            await _screenFunctionRepository.UpdateScreenFunctionAsync(screenFunction);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreenFunction(string id)
        {
            var screen = await _screenFunctionRepository.GetScreenFunctionAsync(id);
            if (screen == null)
            {
                return NotFound("Screen Function not found.");
            }

            await _screenFunctionRepository.DeleteScreenFunctionAsync(id);
            return NoContent();
        }

    }
}
