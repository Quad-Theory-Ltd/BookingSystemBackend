using BookingSundorbon.Features.Repositories.ItemTypeRepository;
using BookingSundorbon.Views.DTOs.ItemTypeView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ItemType
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypeController : ControllerBase
    {

        private readonly IItemTypeRepository _itemTypeRepository;

        public ItemTypeController (IItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveItemTypes()
        {
            var routes = await _itemTypeRepository.GetAllActiveItemTypesAsync();
            return Ok(routes);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateItemType([FromBody] ActiveItemTypeView itemType)
        {
            if (itemType == null)
            {
                return BadRequest("ItemType is Null");
            }
            var itemTypeId = await _itemTypeRepository.CreateItemTypeAsync(itemType);

            return CreatedAtAction(nameof(GetItemType), new { id = itemTypeId }, itemTypeId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetItemType(int id)
        {
            var itemType = await _itemTypeRepository.GetItemTypeAsync(id);
            if (itemType == null)
            {
                return NotFound("ItemType not found.");
            }
            return Ok(itemType);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemType(int id, [FromBody] ActiveItemTypeView itemType)
        {
            if (itemType == null || itemType.Id != id)
            {
                return BadRequest("Item Type Id is Invalid!");
            }
            var existingItemType = await _itemTypeRepository.GetItemTypeAsync(id);
            if (existingItemType == null)
            {
                return BadRequest("Item Type Not Found!");
            }
            await _itemTypeRepository.UpdateItemTypeAsync(itemType);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemType(int id)
        {
            var itemType = await _itemTypeRepository.GetItemTypeAsync(id);
            if (itemType == null)
            {
                return NotFound("Item Type not found.");
            }

            await _itemTypeRepository.DeleteItemTypeAsync(id);
            return NoContent();
        }
    }
}
