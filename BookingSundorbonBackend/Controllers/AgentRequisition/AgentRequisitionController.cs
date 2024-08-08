using BookingSundorbon.Features.Repositories.AgentRepository;
using BookingSundorbon.Features.Repositories.AgentRequisitionRepository;
using BookingSundorbon.Views.DTOs.AgentRequisitionView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.AgentRequisition
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentRequisitionController : ControllerBase
    {

        private readonly IAgentRequisitionRepository _agentRequisitionRepository;

        public AgentRequisitionController(IAgentRequisitionRepository agentRequisitionRepository)
        {
            _agentRequisitionRepository = agentRequisitionRepository;
        }

      


        [HttpPost]
        public async Task<IActionResult> CreateAgentRequisition([FromBody] AgentRequisitionView agentRequisition)
        {
            if (agentRequisition == null)
            {
                return BadRequest("Agent Requisition is Null");
            }
            await _agentRequisitionRepository.CreateAgentRequisitionAsync(agentRequisition);

            return Ok("Agent Requisition Inserted");

            //return CreatedAtAction(nameof(GetAgentRequisition), new { id = agentRequisitionId }, agentRequisitionId);
        }

        [HttpGet("GetAllAgentRequisition")]
        public async Task<IActionResult> GetAllAgentRequisition()
        {
            var agent = await _agentRequisitionRepository.GetAllAgentRequisitionAsync();
            return Ok(agent);
        }

        [HttpGet("GetAllAgentRequisitionWithAgentInfo")]
        public async Task<IActionResult> GetAllAgentRequisitionWithAgentInfo()
        {
            var agent = await _agentRequisitionRepository.GetAllAgentRequisitionWithAgentInfoAsync();
            return Ok(agent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgentRequisition(int id)
        {
            var agent = await _agentRequisitionRepository.GetAgentRequisitionAsync(id);
            if (agent == null)
            {
                return NotFound("Agent Requisition not found.");
            }
            return Ok(agent);
        }




    }
}
