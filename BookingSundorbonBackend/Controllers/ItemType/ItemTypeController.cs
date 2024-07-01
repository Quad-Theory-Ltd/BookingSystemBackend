using BookingSundorbon.Features.Repositories.ItemTypeRepository;
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
    }
}
