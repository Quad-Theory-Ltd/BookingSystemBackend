using BookingSundorbon.Features.Repositories.DistrictRepository;
using BookingSundorbon.Views.DTOs.ActiveDistrictView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.District
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictController(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveDistricts()
        {
            var districts = await _districtRepository.GetAllDistrictsAsync();
            return Ok(districts);
        }
    }
}
