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
            await _agentRepository.CreateAgentAsync(agent);

            return NoContent();

           // return CreatedAtAction(nameof(GetAgent), new { id = agentId }, agentId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAgent(int id)
        {
            var agent = await _agentRepository.GetAgentAsync(id);
            if (agent == null)
            {
                return NotFound("Agent not found.");
            }
            return Ok(agent);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgent(int id, [FromBody] AgentView agent)
        {
            if (agent == null || agent.Id != id)
            {
                return BadRequest("Agent Id is Invalid!");
            }
            var existingAgent = await _agentRepository.GetAgentAsync(id);
            if (existingAgent == null)
            {
                return BadRequest("Agent Not Found!");
            }
            await _agentRepository.UpdateAgentAsync(agent);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            var agent = await _agentRepository.GetAgentAsync(id);
            if (agent == null)
            {
                return NotFound("Agent not found.");
            }

            await _agentRepository.DeleteAgentAsync(id);
            return NoContent();
        }

    }
}
