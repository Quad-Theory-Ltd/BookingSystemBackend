using BookingSundorbon.Features.Repositories.ReceiveRepository;
using BookingSundorbon.Views.DTOs.ReceiveView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Receive
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveController : ControllerBase
    {
        private readonly IReceiveRepository _receiveRepository;

        public ReceiveController(IReceiveRepository receiveRepository)
        {
            _receiveRepository = receiveRepository;
        }
     


        [HttpPost]
        public async Task<IActionResult> CreateReceive([FromBody] ReceiveView receive)
        {
            if (receive == null)
            {
                return BadRequest("Receive is Null");
            }
            var receiveId = await _receiveRepository.CreateReceiveAsync(receive);

            //return CreatedAtAction(nameof(GetReceive), new { id = receiveId }, receiveId);
            return Ok("Receive Created");
        }
        

    }
}
