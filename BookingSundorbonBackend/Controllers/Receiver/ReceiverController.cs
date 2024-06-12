using BookingSundorbon.Features.Repositories.ReceiverRepository;
using BookingSundorbon.Views.DTOs.ReceiverView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Receiver
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IReceiverRepository _receiverRepository;

        public ReceiverController(IReceiverRepository receiverRepository)
        {
            _receiverRepository = receiverRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateReceiver([FromBody] ReceiverView receiver)
        {
            if (receiver == null)
            {
                return BadRequest("Receiver information is null.");
            }

            var newSenderId = await _receiverRepository.CreateReceiverAsync(receiver);
            return CreatedAtAction(nameof(GetReceiver), new { id = newSenderId }, newSenderId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiver(long id)
        {
            var receiver = await _receiverRepository.GetReceiverAsync(id);
            if (receiver == null)
            {
                return NotFound("Receiver information not found.");
            }
            return Ok(receiver);
        }
    }
}
