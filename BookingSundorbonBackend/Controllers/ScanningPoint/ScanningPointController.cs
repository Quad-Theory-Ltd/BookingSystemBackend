using BookingSundorbon.Features.Repositories.ScanningPointRepository;
using BookingSundorbon.Views.DTOs.ScanningPointView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ScanningPoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanningPointController : ControllerBase
    {

        private readonly IScanningPointRepository _scanningPointRepository;

        public ScanningPointController(IScanningPointRepository scanningPointRepository)
        {
            _scanningPointRepository = scanningPointRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveScanningPoints()
        {
            var scanningPoint = await _scanningPointRepository.GetAllActiveScanningPointsAsync();
            return Ok(scanningPoint);
        }


        [HttpPost]
        public async Task<IActionResult> CreateScanningPoint([FromBody] ScanningPointView scanningPoint)
        {
            if (scanningPoint == null)
            {
                return BadRequest("ScanningPoint is Null");
            }
            var scanningPointId = await _scanningPointRepository.CreateScanningPointAsync(scanningPoint);

            return CreatedAtAction(nameof(GetScanningPoint), new { id = scanningPointId }, scanningPointId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetScanningPoint(int id)
        {
            var scanningPoint = await _scanningPointRepository.GetScanningPointAsync(id);
            if (scanningPoint == null)
            {
                return NotFound("ScanningPoint not found.");
            }
            return Ok(scanningPoint);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScanningPoint(int id, [FromBody] ScanningPointView scanningPoint)
        {
            if (scanningPoint == null || scanningPoint.Id != id)
            {
                return BadRequest("ScanningPoint Id is Invalid!");
            }
            var existingScanningPoint = await _scanningPointRepository.GetScanningPointAsync(id);
            if (existingScanningPoint == null)
            {
                return BadRequest("ScanningPoint Not Found!");
            }
            await _scanningPointRepository.UpdateScanningPointAsync(scanningPoint);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScanningPoint(int id)
        {
            var scanningPoint = await _scanningPointRepository.GetScanningPointAsync(id);
            if (scanningPoint == null)
            {
                return NotFound("ScanningPoint not found.");
            }

            await _scanningPointRepository.DeleteScanningPointAsync(id);
            return NoContent();
        }

    }
}
