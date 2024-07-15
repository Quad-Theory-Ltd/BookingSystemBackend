using BookingSundorbon.Features.Repositories.BarcodeStatusDetailRepository;
using BookingSundorbon.Views.DTOs.BarcodeStatusDetailView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.BarcodeStatusDetail
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeStatusDetailController : ControllerBase
    {

        private readonly IBarcodeStatusDetailRepository _barcodeStatusDetailRepository;

        public BarcodeStatusDetailController(IBarcodeStatusDetailRepository barcodeStatusDetailRepository)
        {
            _barcodeStatusDetailRepository = barcodeStatusDetailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveBarcodeStatusDetails()
        {
            var barcodeStatusDetail = await _barcodeStatusDetailRepository.GetAllActiveBarcodeStatusDetailAsync();
            return Ok(barcodeStatusDetail);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBarcodeStatusDetail([FromBody] BarcodeStatusDetailView barcodeStatusDetail)
        {
            if (barcodeStatusDetail == null)
            {
                return BadRequest("Barcode Status Detail is Null");
            }
            var barcodeStatusDetailId = await _barcodeStatusDetailRepository.CreateBarcodeStatusDetailAsync(barcodeStatusDetail);

            return CreatedAtAction(nameof(GetBarcodeStatusDetail), new { id = barcodeStatusDetailId }, barcodeStatusDetailId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetBarcodeStatusDetail(int id)
        {
            var barcodeStatusDetail = await _barcodeStatusDetailRepository.GetBarcodeStatusDetailAsync(id);
            if (barcodeStatusDetail == null)
            {
                return NotFound("Barcode Status Detail not found.");
            }
            return Ok(barcodeStatusDetail);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBarcodeStatusDetail(int id, [FromBody] BarcodeStatusDetailView barcodeStatusDetail)
        {
            if (barcodeStatusDetail == null || barcodeStatusDetail.Id != id)
            {
                return BadRequest("Barcode StatusDetail Id is Invalid!");
            }
            var existingBarcodeStatusDetail = await _barcodeStatusDetailRepository.GetBarcodeStatusDetailAsync(id);
            if (existingBarcodeStatusDetail == null)
            {
                return BadRequest("Barcode Status Detail Not Found!");
            }
            await _barcodeStatusDetailRepository.UpdateBarcodeStatusDetailAsync(barcodeStatusDetail);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarcodeStatusDetail(int id)
        {
            var barcodeStatusDetail = await _barcodeStatusDetailRepository.GetBarcodeStatusDetailAsync(id);
            if (barcodeStatusDetail == null)
            {
                return NotFound("Barcode Status Detail not found.");
            }

            await _barcodeStatusDetailRepository.DeleteBarcodeStatusDetailAsync(id);
            return NoContent();
        }

    }
}
