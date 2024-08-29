using BookingSundorbon.Features.Repositories.AgentRequisitionRepository;
using BookingSundorbon.Features.Repositories.IssueRepository;
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

            return CreatedAtAction(nameof(GetReceiveByReceiveNo), new { receiveNo = receiveId }, receiveId);
           
        }

        [HttpGet("GetAllReceive")]
        public async Task<IActionResult> GetAllReceive()
        {
            var receive = await _receiveRepository.GetAllReceiveAsync();
            return Ok(receive);
        }

        [HttpGet("{receiveNo}")]
        public async Task<IActionResult> GetReceiveByReceiveNo(int receiveNo)
        {
            var receive = await _receiveRepository.GetReceiveAsync(receiveNo);
            if (receive == null)
            {
                return NotFound("Receive not found.");
            }
            return Ok(receive);
        }

        [HttpGet("GetNextReceiveNo")]
        public async Task<IActionResult> GetNextReceiveNo()
        {
            var receiveNo = await _receiveRepository.GetNextReceiveNoAsync();
            if (receiveNo == null)
            {
                return NotFound("Receive No Not found.");
            }
            return Ok(receiveNo);
        }

    }
}
