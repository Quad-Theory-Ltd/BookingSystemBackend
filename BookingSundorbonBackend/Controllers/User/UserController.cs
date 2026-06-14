using BookingSundorbon.Features.Repositories.ApplicationUserRepository;
using BookingSundorbon.Features.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public UserController(
            IUserRepository userRepository,
            IApplicationUserRepository applicationUserRepository)
        {
            _userRepository = userRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var user = await _userRepository.GetAllEmployeeAsync();
            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userRepository.GetAllUsersAsync();
            return Ok(user);
        }

        [HttpGet("GetAdminUserDetails")]
        public async Task<IActionResult> GetAdminUserDetails()
        {
            var admins = await _applicationUserRepository.GetAdminUserDetailsAsync();
            return Ok(admins);
        }
    }
}
