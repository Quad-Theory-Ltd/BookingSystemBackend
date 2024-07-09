using BookingSundorbon.Features.Repositories.MeasurementUnitRepository;
using BookingSundorbon.Views.DTOs.MeasurementUnitView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementUnitController : ControllerBase
    {
        private readonly  IMeasurementUnitRepository _measurementUnitRepository;

        public MeasurementUnitController(IMeasurementUnitRepository measurementUnitRepository)
        {
            _measurementUnitRepository = measurementUnitRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasurementUnit([FromBody] CreateMeasurementUnitView measurementUnit)
        {
            if (measurementUnit == null) { 
                return BadRequest("Measurement Unit is Null");
            }

            var measurementUnitId = await _measurementUnitRepository.CreateMeasurementUnitAsync(measurementUnit);
            return Ok(measurementUnitId);
        }
    }
}
