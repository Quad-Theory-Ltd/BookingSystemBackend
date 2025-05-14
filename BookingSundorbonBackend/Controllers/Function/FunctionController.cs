using BookingSundorbon.Features.Repositories.FunctionRepository;
using BookingSundorbon.Views.DTOs.FunctionView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Function
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {

        private readonly IFunctionRepository _functionRepository;

        public FunctionController(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveFunction()
        {
            var function = await _functionRepository.GetAllActiveFunctionAsync();
            return Ok(function);
        }


        [HttpPost]
        public async Task<IActionResult> CreateFunction([FromBody] FunctionView function)
        {
            if (function == null)
            {
                return BadRequest("Function is Null");
            }
            await _functionRepository.CreateFunctionAsync(function);

            return Created("", "Created");

           // return CreatedAtAction(nameof(GetFunction), new { id = functionId }, functionId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetFunction(string id)
        {
            var function = await _functionRepository.GetFunctionAsync(id);
            if (function == null)
            {
                return NotFound("Function not found.");
            }
            return Ok(function);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFunction(string id, [FromBody] FunctionView function)
        {
            if (function == null || function.Id != id)
            {
                return BadRequest(" Function Id is Invalid!");
            }
            var existingFunction = await _functionRepository.GetFunctionAsync(id);
            if (existingFunction == null)
            {
                return BadRequest(" Function Not Found!");
            }
            await _functionRepository.UpdateFunctionAsync(function);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFunction(string id)
        {
            var function = await _functionRepository.GetFunctionAsync(id);
            if (function == null)
            {
                return NotFound(" Function not found.");
            }

            await _functionRepository.DeleteFunctionAsync(id);
            return NoContent();
        }

    }
}
