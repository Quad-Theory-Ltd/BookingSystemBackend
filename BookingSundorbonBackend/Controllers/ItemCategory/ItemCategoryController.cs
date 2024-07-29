using BookingSundorbon.Features.Repositories.ItemCategoryRepository;
using BookingSundorbon.Views.DTOs.ItemCategoryView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ItemCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {

        private readonly IItemCategoryRepository _itemCategoryRepository;

        public ItemCategoryController(IItemCategoryRepository itemCategoryRepository)
        {
            _itemCategoryRepository = itemCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveItemCategorys()
        {
            var itemCategory = await _itemCategoryRepository.GetAllActiveItemCategoriesAsync();
            return Ok(itemCategory);
        }


        [HttpPost]
        public async Task<IActionResult> CreateItemCategory([FromBody] ItemCategoryView itemCategory)
        {
            if (itemCategory == null)
            {
                return BadRequest("ItemCategory is Null");
            }
            var itemCategoryId = await _itemCategoryRepository.CreateItemCategoryAsync(itemCategory);

            return CreatedAtAction(nameof(GetItemCategory), new { id = itemCategoryId }, itemCategoryId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetItemCategory(int id)
        {
            var itemCategory = await _itemCategoryRepository.GetItemCategoryAsync(id);
            if (itemCategory == null)
            {
                return NotFound("ItemCategory not found.");
            }
            return Ok(itemCategory);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemCategory(int id, [FromBody] ItemCategoryView itemCategory)
        {
            if (itemCategory == null || itemCategory.Id != id)
            {
                return BadRequest(" ItemCategory Id is Invalid!");
            }
            var existingItemCategory = await _itemCategoryRepository.GetItemCategoryAsync(id);
            if (existingItemCategory == null)
            {
                return BadRequest(" ItemCategory Not Found!");
            }
            await _itemCategoryRepository.UpdateItemCategoryAsync(itemCategory);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCategory(int id)
        {
            var itemCategory = await _itemCategoryRepository.GetItemCategoryAsync(id);
            if (itemCategory == null)
            {
                return NotFound("ItemCategory not found.");
            }

            await _itemCategoryRepository.DeleteItemCategoryAsync(id);
            return NoContent();
        }

    }
}
