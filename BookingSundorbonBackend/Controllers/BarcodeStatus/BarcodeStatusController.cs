using BookingSundorbon.Features.Repositories.BarcodeStatusRepository;
using BookingSundorbon.Views.DTOs.BarcodeStatusView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.BarcodeStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeStatusController : ControllerBase
    {

        private readonly IBarcodeStatusRepository _barcodeStatusRepository;

        public BarcodeStatusController(IBarcodeStatusRepository barcodeStatusRepository)
        {
            _barcodeStatusRepository = barcodeStatusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveBarcodeStatuss()
        {
            var barcodeStatus = await _barcodeStatusRepository.GetAllActiveBarcodeStatusAsync();
            return Ok(barcodeStatus);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBarcodeStatus([FromBody] BarcodeStatusView barcodeStatus)
        {
            if (barcodeStatus == null)
            {
                return BadRequest("BarcodeStatus is Null");
            }
            var barcodeStatusId = await _barcodeStatusRepository.CreateBarcodeStatusAsync(barcodeStatus);

            return CreatedAtAction(nameof(GetBarcodeStatus), new { id = barcodeStatusId }, barcodeStatusId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetBarcodeStatus(int id)
        {
            var barcodeStatus = await _barcodeStatusRepository.GetBarcodeStatusAsync(id);
            if (barcodeStatus == null)
            {
                return NotFound("BarcodeStatus not found.");
            }
            return Ok(barcodeStatus);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBarcodeStatus(int id, [FromBody] BarcodeStatusView barcodeStatus)
        {
            if (barcodeStatus == null || barcodeStatus.Id != id)
            {
                return BadRequest("Barcode Status Id is Invalid!");
            }
            var existingBarcodeStatus = await _barcodeStatusRepository.GetBarcodeStatusAsync(id);
            if (existingBarcodeStatus == null)
            {
                return BadRequest("Barcode Status Not Found!");
            }
            await _barcodeStatusRepository.UpdateBarcodeStatusAsync(barcodeStatus);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarcodeStatus(int id)
        {
            var barcodeStatus = await _barcodeStatusRepository.GetBarcodeStatusAsync(id);
            if (barcodeStatus == null)
            {
                return NotFound("Barcode Status not found.");
            }

            await _barcodeStatusRepository.DeleteBarcodeStatusAsync(id);
            return NoContent();
        }

    }
}
