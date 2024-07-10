using BookingSundorbon.Features.Repositories.ProhibitedItemRepository;
using BookingSundorbon.Views.DTOs.ProhibitedItemView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ProhibitedItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProhibitedItemController : ControllerBase
    {
        private readonly IProhibitedItemRepository _prohibitedItemRepository;

        public ProhibitedItemController(IProhibitedItemRepository prohibitedItemRepository)
        {
            _prohibitedItemRepository = prohibitedItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveProhitedItems()
        {
            var prohibitedItems = await _prohibitedItemRepository.GetAllActiveProhibitedItemsAsync();
            return Ok(prohibitedItems);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProhibitedItem([FromBody] ProhibitedItemView prohibitedItem)
        {
            if (prohibitedItem == null)
            {

                return BadRequest("Prohibited item is null!");
            }
            var itemId = await _prohibitedItemRepository.CreateProhibitedItemAsync(prohibitedItem);

            return CreatedAtAction(nameof(GetProhibitedItem), new {id = itemId}, itemId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProhibitedItem(int id)
        {
            var prohibiteditem = await _prohibitedItemRepository.GetProhibitedItemAsync(id);
            if (prohibiteditem == null)
            {
                return NotFound("Prohibited item data not found!.");
            }
            return Ok(prohibiteditem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProhibitedItem(int id, [FromBody] ProhibitedItemView prohibiteditem)
        {
            if (prohibiteditem == null || prohibiteditem.Id != id)
            {
                return BadRequest("ProhibitedItem Data is Invalid!");
            }
            var existingProhibitedItem = await _prohibitedItemRepository.GetProhibitedItemAsync(id);
            if (existingProhibitedItem == null)
            {
                return BadRequest("ProhibitedItem data Not Found!");
            }
            await _prohibitedItemRepository.UpdateProhibitedItemAsync(prohibiteditem);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProhibitedItem(int id)
        {
            var prohibiteditem = await _prohibitedItemRepository.GetProhibitedItemAsync(id);
            if (prohibiteditem == null)
            {
                return NotFound("ProhibitedItem data not found.");
            }

            await _prohibitedItemRepository.DeleteProhibitedItemAsync(id);
            return NoContent();
        }
    }

}