using BookingSundorbon.Features.Repositories.ParcelContentRepository;
using BookingSundorbon.Views.DTOs.ActiveParcelContentView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ParcelContent
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelContentController : ControllerBase
    {
        private readonly IParcelContentRepository _parcelContentRepository;

        public ParcelContentController(IParcelContentRepository parcelContentRepository)
        {
            _parcelContentRepository = parcelContentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveParcelContents()
        {
            var parcelContents = await _parcelContentRepository.GetAllActiveParcelContentsAsync();
            return Ok(parcelContents);
        }
    }
}
