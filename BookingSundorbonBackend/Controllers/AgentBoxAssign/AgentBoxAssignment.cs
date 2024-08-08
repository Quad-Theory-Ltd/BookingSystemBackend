using BookingSundorbon.Features.Repositories.AgentBoxAssignmentRepository;
using BookingSundorbon.Features.Repositories.AgentRequisitionRepository;
using BookingSundorbon.Views.DTOs.AgentBoxAssignmentView;
using BookingSundorbon.Views.DTOs.AgentRequisitionView;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.AgentBoxAssign
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class AgentBoxAssignment : ControllerBase
        {

            private readonly IAgentBoxAssignmentRepository _agentBoxAssignmentRepository;

            public AgentBoxAssignment(IAgentBoxAssignmentRepository agentBoxAssignmentRepository)
            {
             _agentBoxAssignmentRepository = agentBoxAssignmentRepository;
            }


            [HttpGet("GetAllAgentBoxAssignment")]
            public async Task<IActionResult> GetAllAgentBoxAssignment()
            {
                var agent = await _agentBoxAssignmentRepository.GetAllAgentBoxAssignment();
                return Ok(agent);
            }

        [HttpPost("CreateAgentBoxAssignment")]
        public async Task<IActionResult> CreateAgentBoxAssignment([FromBody] AgentBoxAssignmentView agentBoxAssignment)
        {
            if (agentBoxAssignment == null)
            {
                return BadRequest("Invalid data.");
            }

            await _agentBoxAssignmentRepository.CreateAgentBoxAssignmentAsync(agentBoxAssignment);
            return Ok("Agent Box Assignment created successfully.");
        }

    }
    }
