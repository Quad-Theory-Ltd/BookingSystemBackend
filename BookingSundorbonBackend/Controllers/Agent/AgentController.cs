using BookingSundorbon.Features.Repositories.AgentRepository;
using BookingSundorbon.Views.DTOs.AgentView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Agent
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {

        private readonly IAgentRepository _agentRepository;

        public AgentController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveAgent()
        {
            var agent = await _agentRepository.GetAllActiveAgentAsync();
            return Ok(agent);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAgent([FromBody] AgentView agent)
        {
            if (agent == null)
            {
                return BadRequest("Agent is Null");
            }
            var isAgentIdExist = await _agentRepository.GetAgentAsync(agent.UserId);

            if (isAgentIdExist != null) {
                return BadRequest("This User is Already an Agent");
            }

            await _agentRepository.CreateAgentAsync(agent);

            return Created("", "Created");

           // return CreatedAtAction(nameof(GetAgent), new { id = agentId }, agentId);
        }

        [HttpGet("{userId}")]

        public async Task<IActionResult> GetAgent(int userId)
        {
            var agent = await _agentRepository.GetAgentAsync(userId);
            if (agent == null)
            {
                return NotFound("Agent not found.");
            }
            return Ok(agent);
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAgent(int userId, [FromBody] AgentView agent)
        {
            if (agent == null || agent.UserId != userId)
            {
                return BadRequest("userId is Invalid!");
            }
            var existingAgent = await _agentRepository.GetAgentAsync(userId);
            if (existingAgent == null)
            {
                return BadRequest("Agent Not Found!");
            }
            await _agentRepository.UpdateAgentAsync(agent);
            return NoContent();
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAgent(int userId)
        {
            var agent = await _agentRepository.GetAgentAsync(userId);
            if (agent == null)
            {
                return NotFound("Agent not found.");
            }

            await _agentRepository.DeleteAgentAsync(userId);
            return NoContent();
        }

    }
}
