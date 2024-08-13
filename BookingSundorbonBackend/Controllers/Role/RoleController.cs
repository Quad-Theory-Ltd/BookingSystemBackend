using BookingSundorbon.Features.Repositories.RoleRepository;
using BookingSundorbon.Views.DTOs.RoleView;
using BookingSundorbon.Views.DTOs.ScreenView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleView role)
        {
            if (role == null)
            {
                return BadRequest("Role Service is Null");
            }
            await _roleRepository.CreateRoleAsynce(role);

            return Created("", "Created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveRoles()
        {
            var roles = await _roleRepository.GetAllActiveRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _roleRepository.GetRoleAsync(id);
            if (role == null)
            {
                return NotFound("Role data not found!.");
            }
            return Ok(role);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleView role)
        {
            if (role == null || role.Id != id)
            {
                return BadRequest("Role Id is Invalid!");
            }
            var existingScreen = await _roleRepository.GetRoleAsync(id);
            if (existingScreen == null)
            {
                return BadRequest("Role Not Found!");
            }
            await _roleRepository.UpdateRoleAsync(role);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleRepository.GetRoleAsync(id);
            if (role == null)
            {
                return NotFound("Role data not found.");
            }

            await _roleRepository.DeleteRoleAsync(id);
            return NoContent();
        }
    }

}

