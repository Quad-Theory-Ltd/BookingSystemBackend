using BookingSundorbon.Features.Repositories.AgentBoxAssignRepository;
using BookingSundorbon.Features.Repositories.AgentRepository;
using BookingSundorbon.Features.Repositories.AgentBoxAssignRepository;
using BookingSundorbon.Views.DTOs.AgentBoxAssignView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.AgentBoxAssign
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentBoxAssignController : ControllerBase
    {

        private readonly IAgentBoxAssignRepository _agentBoxAssignRepository;

        public AgentBoxAssignController(IAgentBoxAssignRepository agentBoxAssignRepository)
        {
           _agentBoxAssignRepository = agentBoxAssignRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveAgentBoxAssigns()
        {
            var agentBoxAssign = await _agentBoxAssignRepository.GetAllActiveAgentBoxAssignsAsync();
            return Ok(agentBoxAssign);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAgentBoxAssign([FromBody] AgentBoxAssignView agentBoxAssign)
        {
            if (agentBoxAssign == null)
            {
                return BadRequest("AgentBoxAssign is Null");
            }
            var agentBoxAssignId = await _agentBoxAssignRepository.CreateAgentBoxAssignAsync(agentBoxAssign);

            return CreatedAtAction(nameof(GetAgentBoxAssign), new { id = agentBoxAssignId }, agentBoxAssignId);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetAgentBoxAssign(int id)
        {
            var agentBoxAssign = await _agentBoxAssignRepository.GetAgentBoxAssignAsync(id);
            if (agentBoxAssign == null)
            {
                return NotFound("AgentBoxAssign not found.");
            }
            return Ok(agentBoxAssign);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgentBoxAssign(int id, [FromBody] AgentBoxAssignView agentBoxAssign)
        {
            if (agentBoxAssign == null || agentBoxAssign.Id != id)
            {
                return BadRequest(" AgentBoxAssign Id is Invalid!");
            }
            var existingAgentBoxAssign = await _agentBoxAssignRepository.GetAgentBoxAssignAsync(id);
            if (existingAgentBoxAssign == null)
            {
                return BadRequest(" AgentBoxAssign Not Found!");
            }
            await _agentBoxAssignRepository.UpdateAgentBoxAssignAsync(agentBoxAssign);
            return NoContent();
        }
        

        [HttpGet("AgentBoxAssignDetailsByAgentId/{id}")]
        public async Task<IActionResult> AgentBoxAssignDetailsByAgentId(int id)
        {
            var count = await _agentBoxAssignRepository.AgentBoxAssignDetailsByAgentIdAsync(id);
            return Ok(count);
        }

        [HttpGet("AgentBoxAssignByDetailsById/{id}")]
        public async Task<IActionResult> AgentBoxAssignByDetailsById(int id)
        {
            var agentBox = await _agentBoxAssignRepository.AgentBoxAssignByDetailsByIdAsync(id);
            return Ok(agentBox);
        }
    }
}
