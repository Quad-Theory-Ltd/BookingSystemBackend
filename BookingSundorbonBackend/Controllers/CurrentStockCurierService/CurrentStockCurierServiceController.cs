using BookingSundorbon.Features.Repositories.CurrentStockCurierServiceRepository;
using BookingSundorbon.Views.DTOs.CurrentStockCurierServiceView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.CurrentStockCurierService
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentStockCurierServiceController : ControllerBase
    {

        private readonly ICurrentStockCurierServiceRepository _currentStockCurierServiceRepository;

        public CurrentStockCurierServiceController(ICurrentStockCurierServiceRepository currentStockCurierServiceRepository)
        {
            _currentStockCurierServiceRepository = currentStockCurierServiceRepository;
        }

        [HttpGet("GetAllCurrentStockCurierServices")]
        public async Task<IActionResult> GetAllCurrentStockCurierServices()
        {
            var currentStockCurierService = await _currentStockCurierServiceRepository.GetAllCurrentStockCurierServicesAsync();
            return Ok(currentStockCurierService);
        }


        [HttpPost("CreateCurrentStockCurierService")]
        public async Task<IActionResult> CreateCurrentStockCurierService([FromBody] CurrentStockCurierServiceView currentStockCurierService)
        {
            if (currentStockCurierService == null)
            {
                return BadRequest("CurrentStockCurierService is Null");
            }
            var currentStockCurierServiceId = await _currentStockCurierServiceRepository.CreateCurrentStockCurierServiceAsync(currentStockCurierService);

            return CreatedAtAction(nameof(GetCurrentStockCurierService), new { id = currentStockCurierServiceId }, currentStockCurierServiceId);
        }

        [HttpGet("GetCurrentStockCurierService/{id}")]

        public async Task<IActionResult> GetCurrentStockCurierService(int id)
        {
            var currentStockCurierService = await _currentStockCurierServiceRepository.GetCurrentStockCurierServiceAsync(id);
            if (currentStockCurierService == null)
            {
                return NotFound("CurrentStockCurierService not found.");
            }
            return Ok(currentStockCurierService);
        }


        [HttpPut("UpdateCurrentStockCurierService/{id}")]
        public async Task<IActionResult> UpdateCurrentStockCurierService(int id, [FromBody] CurrentStockCurierServiceView currentStockCurierService)
        {
            if (currentStockCurierService == null || currentStockCurierService.Id != id)
            {
                return BadRequest(" CurrentStockCurierService Id is Invalid!");
            }
            var existingCurrentStockCurierService = await _currentStockCurierServiceRepository.GetCurrentStockCurierServiceAsync(id);
            if (existingCurrentStockCurierService == null)
            {
                return BadRequest(" CurrentStockCurierService Not Found!");
            }
            await _currentStockCurierServiceRepository.UpdateCurrentStockCurierServiceAsync(currentStockCurierService);
            return NoContent();
        }


        [HttpDelete("DeleteCurrentStockCurierService/{id}")]
        public async Task<IActionResult> DeleteCurrentStockCurierService(int id)
        {
            var currentStockCurierService = await _currentStockCurierServiceRepository.GetCurrentStockCurierServiceAsync(id);
            if (currentStockCurierService == null)
            {
                return NotFound("CurrentStockCurierService not found.");
            }

            await _currentStockCurierServiceRepository.DeleteCurrentStockCurierServiceAsync(id);
            return NoContent();
        }

    }
}
