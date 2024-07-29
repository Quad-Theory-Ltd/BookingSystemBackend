using BookingSundorbon.Features.Repositories.AgentBookingRepository;
using BookingSundorbon.Features.Repositories.BranchRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.AgentBooking
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentBookingController : ControllerBase
    {
        private readonly IAgentBookingRepository _agentBookingRepository;

        public AgentBookingController(IAgentBookingRepository agentBookingRepository)
        {
            _agentBookingRepository = agentBookingRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgentBookingCountsByDimension(string id)
        {
            var count = await _agentBookingRepository.GetAgentBookingCountsByDimensionAsync(id);
            if (count == null)
            {
                return NotFound("counts not found.");
            }
            return Ok(count);
        }
    }
}
