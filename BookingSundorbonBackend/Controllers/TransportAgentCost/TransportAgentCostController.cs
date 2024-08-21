using BookingSundorbon.Features.Repositories.TransportAgentCostRepository;
using BookingSundorbon.Views.DTOs.TransportAgentCostView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.TransportAgentCost
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportAgentCostController : ControllerBase
    {

        private readonly ITransportAgentCostRepository _transportAgentCostRepository;

        public TransportAgentCostController(ITransportAgentCostRepository transportAgentCostRepository)
        {
            _transportAgentCostRepository = transportAgentCostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveTransportAgentCosts()
        {
            var transportAgentCost = await _transportAgentCostRepository.GetAllTransportAgentCostsAsync();
            return Ok(transportAgentCost);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTransportAgentCost([FromBody] List<TransportAgentCostView> transportAgentCosts)
        {
            if (transportAgentCosts == null || !transportAgentCosts.Any())
            {
                return BadRequest("TransportAgentCosts is Null or Empty");
            }

            var createdIds = new List<int>();

            foreach (var transportAgentCost in transportAgentCosts)
            {
                var transportAgentCostId = await _transportAgentCostRepository.CreateTransportAgentCostAsync(transportAgentCost);
                createdIds.Add(transportAgentCostId);
            }

            return Created("", "Transport Agent Cost Created");
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetTransportAgentCost(int id)
        {
            var transportAgentCost = await _transportAgentCostRepository.GetTransportAgentCostAsync(id);
            if (transportAgentCost == null)
            {
                return NotFound("TransportAgentCost not found.");
            }
            return Ok(transportAgentCost);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransportAgentCost(int id, [FromBody] TransportAgentCostView transportAgentCost)
        {
            if (transportAgentCost == null || transportAgentCost.Id != id)
            {
                return BadRequest(" TransportAgentCost Id is Invalid!");
            }
            var existingTransportAgentCost = await _transportAgentCostRepository.GetTransportAgentCostAsync(id);
            if (existingTransportAgentCost == null)
            {
                return BadRequest(" TransportAgentCost Not Found!");
            }
            await _transportAgentCostRepository.UpdateTransportAgentCostAsync(transportAgentCost);
            return NoContent();
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTransportAgentCost(int id)
        //{
        //    var transportAgentCost = await _transportAgentCostRepository.GetTransportAgentCostAsync(id);
        //    if (transportAgentCost == null)
        //    {
        //        return NotFound("TransportAgentCost not found.");
        //    }

        //    await _transportAgentCostRepository.DeleteTransportAgentCostAsync(id);
        //    return NoContent();
        //}

    }
}
