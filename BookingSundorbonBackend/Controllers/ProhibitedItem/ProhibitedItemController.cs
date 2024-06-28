using BookingSundorbon.Features.Repositories.ProductTypeRepository;
using BookingSundorbon.Features.Repositories.ProhibitedItemRepository;
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
            var prohibitedItems = await _prohibitedItemRepository.GetAllActiveProhitedItemsAsync();
            return Ok(prohibitedItems);
        }
    }
}
