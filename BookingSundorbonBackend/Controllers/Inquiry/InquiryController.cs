using BookingSundorbon.Features.Repositories.InquiryRepository;
using BookingSundorbon.Views.DTOs.InquiryView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Inquiry
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly IInquiryRepository _inquiryRepository;

        public InquiryController(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        [HttpPost("CreateInquiry")]
        public async Task<IActionResult> CreateInquiry([FromBody] InquiryView inquiry)
        {
            if (inquiry == null)
            {
                return BadRequest("Inquiry is null.");
            }
            await _inquiryRepository.InsertInquiryAsync(inquiry);
            return Ok("Inquiry created successfully.");
        }

        [HttpGet("GetInquiry/{id}")]
        public async Task<IActionResult> GetInquiry(int id)
        {
            var inquiry = await _inquiryRepository.GetInquiryByIdAsync(id);
            if (inquiry == null)
            {
                return NotFound("Inquiry not found.");
            }
            return Ok(inquiry);
        }

        [HttpGet("GetInquiries")]
        public async Task<IActionResult> GetInquiries(string? status = null, int page = 1, int pageSize = 20)
        {
            var inquiries = await _inquiryRepository.GetInquiriesAsync(status, page, pageSize);
            return Ok(inquiries);
        }

        [HttpPut("UpdateInquiry/{id}")]
        public async Task<IActionResult> UpdateInquiry(int id, [FromBody] InquiryView inquiry)
        {
            if (inquiry == null || inquiry.Id != id)
            {
                return BadRequest("Inquiry data is invalid.");
            }
            var existingInquiry = await _inquiryRepository.GetInquiryByIdAsync(id);
            if (existingInquiry == null)
            {
                return NotFound("Inquiry not found.");
            }
            await _inquiryRepository.UpdateInquiryAsync(inquiry);
            return Ok("Inquiry updated successfully.");
        }
    }
}
