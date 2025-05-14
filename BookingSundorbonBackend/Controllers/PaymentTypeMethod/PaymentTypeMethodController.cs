using BookingSundorbon.Features.Repositories.PaymentTypeMethodRepository;
using BookingSundorbon.Views.DTOs.PaymentTypeMethodView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.PaymentTypeMethod
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeMethodController : ControllerBase
    {

        private readonly IPaymentTypeMethodRepository _paymentTypeMethodRepository;

        public PaymentTypeMethodController(IPaymentTypeMethodRepository paymentTypeMethodRepository)
        {
            _paymentTypeMethodRepository = paymentTypeMethodRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivePaymentTypeMethods()
        {
            var paymentTypeMethod = await _paymentTypeMethodRepository.GetAllActivePaymentTypeMethodsAsync();
            return Ok(paymentTypeMethod);
        }


    }
}
