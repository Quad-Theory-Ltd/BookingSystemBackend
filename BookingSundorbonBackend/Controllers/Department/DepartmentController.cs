using BookingSundorbon.Features.Repositories.DepartmentRepository;
using BookingSundorbon.Views.DTOs.DepartmentView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Department
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveDepartments()
        {
            var Department = await _departmentRepository.GetAllDepartmentsAsync();
            return Ok(Department);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentView department)
        {
            if (department == null)
            {
                return BadRequest("Department is Null");
            }
            var DepartmentId = await _departmentRepository.CreateDepartmentAsync(department);

            return CreatedAtAction(nameof(GetDepartment), new { id = DepartmentId }, DepartmentId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _departmentRepository.GetDepartmentAsync(id);
            if (department == null)
            {
                return NotFound("Department not found.");
            }
            return Ok(department);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentView department)
        {
            if (department == null || department.Id != id)
            {
                return BadRequest(" Department Id is Invalid!");
            }
            var existingDepartment = await _departmentRepository.GetDepartmentAsync(id);
            if (existingDepartment == null)
            {
                return BadRequest(" Department Not Found!");
            }
            await _departmentRepository.UpdateDepartmentAsync(department);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var Department = await _departmentRepository.GetDepartmentAsync(id);
            if (Department == null)
            {
                return NotFound(" Department not found.");
            }

            await _departmentRepository.DeleteDepartmentAsync(id);
            return NoContent();
        }

    }
}
