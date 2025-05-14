using BookingSundorbon.Features.Repositories.BoxCurrentStockRepository;
using BookingSundorbon.Views.DTOs.BoxCurrentStockView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.BoxCurrentStock
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxCurrentStockController : ControllerBase
    {

        private readonly IBoxCurrentStockRepository _boxCurrentStockRepository;

        public BoxCurrentStockController(IBoxCurrentStockRepository boxCurrentStockRepository)
        {
            _boxCurrentStockRepository = boxCurrentStockRepository;
        }

        [HttpGet("GetAllBoxCurrentStocks")]
        public async Task<IActionResult> GetAllBoxCurrentStocks()
        {
            var boxCurrentStock = await _boxCurrentStockRepository.GetAllBoxCurrentStocksAsync();
            return Ok(boxCurrentStock);
        }


        [HttpPost(" CreateBoxCurrentStock")]
        public async Task<IActionResult> CreateBoxCurrentStock([FromBody] BoxCurrentStockView boxCurrentStock)
        {
            if (boxCurrentStock == null)
            {
                return BadRequest("BoxCurrentStock is Null");
            }
            var boxCurrentStockId = await _boxCurrentStockRepository.CreateBoxCurrentStockAsync(boxCurrentStock);

            return CreatedAtAction(nameof(GetBoxCurrentStock), new { id = boxCurrentStockId }, boxCurrentStockId);
        }

        [HttpGet("GetBoxCurrentStock/{id}")]

        public async Task<IActionResult> GetBoxCurrentStock(int id)
        {
            var boxCurrentStock = await _boxCurrentStockRepository.GetBoxCurrentStockAsync(id);
            if (boxCurrentStock == null)
            {
                return NotFound("BoxCurrentStock not found.");
            }
            return Ok(boxCurrentStock);
        }


        [HttpPut("UpdateBoxCurrentStock/{id}")]
        public async Task<IActionResult> UpdateBoxCurrentStock(int id, [FromBody] BoxCurrentStockView boxCurrentStock)
        {
            if (boxCurrentStock == null || boxCurrentStock.Id != id)
            {
                return BadRequest(" BoxCurrentStock Id is Invalid!");
            }
            var existingBoxCurrentStock = await _boxCurrentStockRepository.GetBoxCurrentStockAsync(id);
            if (existingBoxCurrentStock == null)
            {
                return BadRequest(" BoxCurrentStock Not Found!");
            }
            await _boxCurrentStockRepository.UpdateBoxCurrentStockAsync(boxCurrentStock);
            return NoContent();
        }


        [HttpDelete("DeleteBoxCurrentStock/{id}")]
        public async Task<IActionResult> DeleteBoxCurrentStock(int id)
        {
            var boxCurrentStock = await _boxCurrentStockRepository.GetBoxCurrentStockAsync(id);
            if (boxCurrentStock == null)
            {
                return NotFound("BoxCurrentStock not found.");
            }

            await _boxCurrentStockRepository.DeleteBoxCurrentStockAsync(id);
            return NoContent();
        }

    }
}
