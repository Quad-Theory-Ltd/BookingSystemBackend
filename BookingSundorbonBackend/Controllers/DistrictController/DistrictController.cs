using BookingSundorbon.Features.Repositories.BranchRepository;
using BookingSundorbon.Features.Repositories.DistrictRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.DistrictController
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
        public async Task<IActionResult> GetDistricts()
        {
            var result = await _districtRepository.GetAllDistrictsAsync();
            if (result == null)
            {
                return NotFound("District not found.");
            }
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetSingleDistrict(int id, bool isActive)
        //{
        //    var result = await _districtRepository.GetDistrictByIdAsync(id, isActive);
        //    if (result == null)
        //    {
        //        return NotFound("District not found.");
        //    }
        //    return Ok(result);
        //}
    }
}
