using BookingSundorbon.Features.Repositories.PaymentRepository;
using BookingSundorbon.Views.DTOs.PaymentView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet("GetAllPayments")]
        public async Task<IActionResult> GetAllActivePayments()
        {
            var payment = await _paymentRepository.GetAllPaymentsAsync();
            return Ok(payment);
        }


        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentView payment)
        {
            if (payment == null)
            {
                return BadRequest("Payment is Null");
            }
            var paymentId = await _paymentRepository.CreatePaymentAsync(payment);

            return CreatedAtAction(nameof(GetPayment), new { id = paymentId }, paymentId);
        }

        [HttpGet("GetPayment/{id}")]

        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _paymentRepository.GetPaymentAsync(id);
            if (payment == null)
            {
                return NotFound("Payment not found.");
            }
            return Ok(payment);
        }


        [HttpPut("UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentView payment)
        {
            if (payment == null || payment.Id != id)
            {
                return BadRequest(" Payment Id is Invalid!");
            }
            var existingPayment = await _paymentRepository.GetPaymentAsync(id);
            if (existingPayment == null)
            {
                return BadRequest(" Payment Not Found!");
            }
            await _paymentRepository.UpdatePaymentAsync(payment);
            return NoContent();
        }


        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _paymentRepository.GetPaymentAsync(id);
            if (payment == null)
            {
                return NotFound("Payment not found.");
            }

            await _paymentRepository.DeletePaymentAsync(id);
            return NoContent();
        }

    }
}
