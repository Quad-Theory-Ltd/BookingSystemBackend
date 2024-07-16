using BookingSundorbon.Features.Repositories.BranchRepository;
using BookingSundorbon.Features.Repositories.MeasurementUnitRepository;
using BookingSundorbon.Views.DTOs.MeasurementUnitView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.MeasurementUnit
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementUnitController : ControllerBase
    {
        private readonly IMeasurementUnitRepository _measurementUnitRepository;

        public MeasurementUnitController(IMeasurementUnitRepository measurementUnitRepository)
        {
            _measurementUnitRepository = measurementUnitRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasurementUnit([FromBody] MeasurementUnitView measurementUnit)
        {
            if (measurementUnit == null)
            {
                return BadRequest("Measurement Unit is Null");
            }

            var newMeasurementUnitId = await _measurementUnitRepository.CreateMeasurementUnitAsync(measurementUnit);
            return CreatedAtAction(nameof(GetMeasurementUnit), new { id = newMeasurementUnitId }, newMeasurementUnitId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurementUnit (int id)
        {
            var measurementUnit = await _measurementUnitRepository.GetMeasurementUnitAsync(id);
            if (measurementUnit == null)
            {
                return NotFound("Measurement Unit not found.");
            }
            return Ok(measurementUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeasurementUnits()
        {
            var measurementUnits = await _measurementUnitRepository.GetAllActiveMeasurementUnitsAsync();
            return Ok(measurementUnits);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurementUnit(int id, [FromBody] MeasurementUnitView measurementUnit)
        {
            if(measurementUnit == null || measurementUnit.Id != id)
            {
                return BadRequest("Measurement Data is Invalid!");
            }
            var existingMeasurementUnit = await _measurementUnitRepository.GetMeasurementUnitAsync(id);
            if(existingMeasurementUnit == null)
            {
                return BadRequest("Measurement Unit Not Found!");
            }
            await _measurementUnitRepository.UpdateMeasurementUnitAsync(measurementUnit);
            return NoContent() ;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurementUnit(int id)
        {
            var measurementUnit = await _measurementUnitRepository.GetMeasurementUnitAsync(id);
            if (measurementUnit == null)
            {
                return NotFound("Measurement Unit not found.");
            }

            await _measurementUnitRepository.DeleteMeasurementUnitAsync(id);
            return NoContent();
        }
    }
}
