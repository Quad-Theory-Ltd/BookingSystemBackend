﻿using BookingSundorbon.Features.Repositories.AgentBoxAssignRepository;
using BookingSundorbon.Features.Repositories.AgentRepository;
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

        [HttpGet("CountAgentBoxAssignByAgentId/{id}")]
        public async Task<IActionResult> CountAgentBoxAssignByAgentId(int id)
        {
            var count = await _agentBoxAssignRepository.CountAgentBoxAssignByAgentIdAsync(id);
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
