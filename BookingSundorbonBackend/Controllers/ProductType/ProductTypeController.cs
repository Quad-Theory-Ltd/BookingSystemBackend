using BookingSundorbon.Features.Repositories.ProductTypeRepository;
using BookingSundorbon.Views.DTOs.ActiveProductTypeView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ProductType
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveProductTypes()
        {
            var productTypes = await _productTypeRepository.GetAllActiveProductTypesAsync();
            return Ok(productTypes);
        }
    }
}
