using BookingSundorbon.Features.Repositories.LoginRepository;
using BookingSundorbon.Views.DTOs.LoginView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingSundorbonBackend.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> userLoginDetails(string userName, string password, int type)
        {
            var res = await _loginRepository.GetAllCustomerListAsync(userName, password, type);
            if (res != null)
            {
                var token = GenerateJwtToken(res);
                return new OkObjectResult(token);
            }
            return BadRequest("User Not Found");
        }
        private string GenerateJwtToken(LoginView login)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim("UserName", login.UserName.ToString()),
                    new Claim("UserId", login.UserId.ToString()),
                    new Claim("RoleId", login.RoleId.ToString()),
                    new Claim("EmployeeId", login.EmployeeId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };

            //authClaims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            // Create a new JWT token with the given claims and signing key
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            // Return the newly created token
            var tokend = new JwtSecurityTokenHandler().WriteToken(token);
            return tokend;
        }
    }
}
