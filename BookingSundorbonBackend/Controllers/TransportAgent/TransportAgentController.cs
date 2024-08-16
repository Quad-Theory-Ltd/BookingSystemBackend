using BookingSundorbon.Features.Repositories.TransportAgentRepository;
using BookingSundorbon.Views.DTOs.TransportAgentView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.TransportAgent
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportAgentController : ControllerBase
    {

        private readonly ITransportAgentRepository _transportAgentRepository;

        public TransportAgentController(ITransportAgentRepository transportAgentRepository)
        {
            _transportAgentRepository = transportAgentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveTransportAgents()
        {
            var transportAgent = await _transportAgentRepository.GetAllActiveTransportAgentsAsync();
            return Ok(transportAgent);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTransportAgent([FromBody] TransportAgentView transportAgent)
        {
            if (transportAgent == null)
            {
                return BadRequest("TransportAgent is Null");
            }
            var transportAgentId = await _transportAgentRepository.CreateTransportAgentAsync(transportAgent);

            return CreatedAtAction(nameof(GetTransportAgent), new { id = transportAgentId }, transportAgentId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetTransportAgent(int id)
        {
            var transportAgent = await _transportAgentRepository.GetTransportAgentAsync(id);
            if (transportAgent == null)
            {
                return NotFound("TransportAgent not found.");
            }
            return Ok(transportAgent);
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTransportAgent(int id, [FromBody] TransportAgentView transportAgent)
        //{
        //    if (transportAgent == null || transportAgent.Id != id)
        //    {
        //        return BadRequest(" TransportAgent Id is Invalid!");
        //    }
        //    var existingTransportAgent = await _transportAgentRepository.GetTransportAgentAsync(id);
        //    if (existingTransportAgent == null)
        //    {
        //        return BadRequest(" TransportAgent Not Found!");
        //    }
        //    await _transportAgentRepository.UpdateTransportAgentAsync(transportAgent);
        //    return NoContent();
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTransportAgent(int id)
        //{
        //    var transportAgent = await _transportAgentRepository.GetTransportAgentAsync(id);
        //    if (transportAgent == null)
        //    {
        //        return NotFound("TransportAgent not found.");
        //    }

        //    await _transportAgentRepository.DeleteTransportAgentAsync(id);
        //    return NoContent();
        //}

    }
}
