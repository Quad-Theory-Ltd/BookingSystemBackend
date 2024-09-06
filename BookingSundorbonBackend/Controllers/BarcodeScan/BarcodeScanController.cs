using BookingSundorbon.Features.Repositories.BarcodeScanRepository;
using BookingSundorbon.Views.DTOs.BarcodeScanView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.BarcodeScan
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeScanController : ControllerBase
    {

        private readonly IBarcodeScanRepository _barcodeScanRepository;

        public BarcodeScanController(IBarcodeScanRepository barcodeScanRepository)
        {
            _barcodeScanRepository = barcodeScanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveBarcodeScans()
        {
            var barcodeScan = await _barcodeScanRepository.GetAllActiveBarcodeScansAsync();
            return Ok(barcodeScan);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBarcodeScan([FromBody] BarcodeScanView barcodeScan)
        {
            if (barcodeScan == null)
            {
                return BadRequest("BarcodeScan is Null");
            }
            var barcodeScanId = await _barcodeScanRepository.CreateBarcodeScanAsync(barcodeScan);

            return CreatedAtAction(nameof(GetBarcodeScan), new { id = barcodeScanId }, barcodeScanId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetBarcodeScan(int id)
        {
            var barcodeScan = await _barcodeScanRepository.GetBarcodeScanAsync(id);
            if (barcodeScan == null)
            {
                return NotFound("BarcodeScan not found.");
            }
            return Ok(barcodeScan);
        }

        [HttpGet("GetAgentBarcodeScan/{userId}")]
        public async Task<IActionResult> GetAgentBarcodeScan(int userId)
        {
            var barcodeScan = await _barcodeScanRepository.GetAgentBarcodeScanAsync(userId);
            if (barcodeScan == null)
            {

                return NotFound("BarcodeScan not found.");
            }
            return Ok(barcodeScan);
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateBarcodeScan(int id, [FromBody] BarcodeScanView barcodeScan)
        //{
        //    if (barcodeScan == null || barcodeScan.Id != id)
        //    {
        //        return BadRequest(" BarcodeScan Id is Invalid!");
        //    }
        //    var existingBarcodeScan = await _barcodeScanRepository.GetBarcodeScanAsync(id);
        //    if (existingBarcodeScan == null)
        //    {
        //        return BadRequest(" BarcodeScan Not Found!");
        //    }
        //    await _barcodeScanRepository.UpdateBarcodeScanAsync(barcodeScan);
        //    return NoContent();
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBarcodeScan(int id)
        //{
        //    var barcodeScan = await _barcodeScanRepository.GetBarcodeScanAsync(id);
        //    if (barcodeScan == null)
        //    {
        //        return NotFound("BarcodeScan not found.");
        //    }

        //    await _barcodeScanRepository.DeleteBarcodeScanAsync(id);
        //    return NoContent();
        //}

    }
}
