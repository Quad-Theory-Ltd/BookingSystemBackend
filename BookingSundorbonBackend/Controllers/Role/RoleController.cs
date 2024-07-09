using BookingSundorbon.Features.Repositories.RoleRepository;
using BookingSundorbon.Views.DTOs.RoleView;
using BookingSundorbon.Views.DTOs.ShippingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController (IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleView role)
        {
            if (role == null)
            {
                return BadRequest("Role Service is Null");
            }
            await _roleRepository.CreateRoleAsynce(role);

            return NoContent();
        }
    }
}
