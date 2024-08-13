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
        public async Task<IActionResult> GetLoginById(string userName, string password, string userType)
        {
            var res = await _loginRepository.GetLoginByIdAsync(userName, password, userType);
            if (res != null)
            {
                var token = GenerateJwtToken(res);
                return Ok(res);
            }
            return BadRequest("User Not Found");
        }
        private string GenerateJwtToken(LoginView login)
        {
            var authClaims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, login.UserName),
                    //new Claim("UserName", login.UserName.ToString()),
                    //new Claim("UserId", login.UserId.ToString()),
                    //new Claim("RoleId", login.RoleId.ToString()),
                    //new Claim("EmployeeId", login.EmployeeId.ToString()),
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


        [HttpPost]
        public async Task<IActionResult> CreateLogin([FromBody] LoginView login)
        {
            if (login == null)
            {
                return BadRequest("LoginId is Null");
            }
            var loginId =  await _loginRepository.CreateLoginAsync(login);

            return Created("",loginId);

            // return CreatedAtAction(nameof(GetLoginId), new { id = loginId }, loginId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLogin(int id, [FromBody] LoginView login)
        {
            if (login == null || login.Id != id)
            {
                return BadRequest("Login Id is Invalid!");
            }
            var existingLogin = await _loginRepository.GetLoginByUserIdAsync(id);
            if (existingLogin == null)
            {
                return BadRequest(" Login Not Found!");
            }
            await _loginRepository.UpdateLoginAsync(login);
            return NoContent();
        }


    }
}
