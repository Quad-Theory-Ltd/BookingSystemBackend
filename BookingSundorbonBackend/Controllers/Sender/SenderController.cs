using BookingSundorbon.Features.Repositories.SenderRepository;
using BookingSundorbon.Views.DTOs.SenderView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Sender
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderRepository _senderRepository;

        public SenderController(ISenderRepository senderRepository)
        {
            _senderRepository = senderRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSender([FromBody] SenderView sender)
        {
            if (sender == null)
            {
                return BadRequest("Sender information is null.");
            }

            var newSenderId = await _senderRepository.CreateSenderAsync(sender);
            return CreatedAtAction(nameof(GetSender), new { id = newSenderId }, newSenderId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSender(long id)
        {
            var sender = await _senderRepository.GetSenderAsync(id);
            if (sender == null)
            {
                return NotFound("Sender information not found.");
            }
            return Ok(sender);
        }
    }
}
