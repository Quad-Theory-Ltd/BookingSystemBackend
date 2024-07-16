using BookingSundorbon.Features.Repositories.DepartmentalOperationRepository;
using BookingSundorbon.Views.DTOs.DepartmentalOperationView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.DepartmentalOperation
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentalOperationController : ControllerBase
    {

        private readonly IDepartmentalOperationRepository _departmentalOperationRepository;

        public DepartmentalOperationController(IDepartmentalOperationRepository departmentalOperationRepository)
        {
            _departmentalOperationRepository = departmentalOperationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentalOperation()
        {
            var departmentalOperation = await _departmentalOperationRepository.GetAllDepartmentalOperationAsync();
            return Ok(departmentalOperation);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartmentalOperation([FromBody] DepartmentalOperationView departmentalOperation)
        {
            if (departmentalOperation == null)
            {
                return BadRequest("Departmental Operation is Null");
            }
            var departmentalOperationId = await _departmentalOperationRepository.CreateDepartmentalOperationAsync(departmentalOperation);

            return CreatedAtAction(nameof(GetDepartmentalOperation), new { id = departmentalOperationId }, departmentalOperationId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDepartmentalOperation(int id)
        {
            var departmentalOperation = await _departmentalOperationRepository.GetDepartmentalOperationAsync(id);
            if (departmentalOperation == null)
            {
                return NotFound("Departmental Operation not found.");
            }
            return Ok(departmentalOperation);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentalOperation(int id, [FromBody] DepartmentalOperationView departmentalOperation)
        {
            if (departmentalOperation == null || departmentalOperation.Id != id)
            {
                return BadRequest("Departmental Operation Id is Invalid!");
            }
            var existingDepartmentalOperation = await _departmentalOperationRepository.GetDepartmentalOperationAsync(id);
            if (existingDepartmentalOperation == null)
            {
                return BadRequest(" Departmental Operation Not Found!");
            }
            await _departmentalOperationRepository.UpdateDepartmentalOperationAsync(departmentalOperation);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentalOperation(int id)
        {
            var departmentalOperation = await _departmentalOperationRepository.GetDepartmentalOperationAsync(id);
            if (departmentalOperation == null)
            {
                return NotFound(" Departmental Operation not found.");
            }

            await _departmentalOperationRepository.DeleteDepartmentalOperationAsync(id);
            return NoContent();
        }

    }
}
