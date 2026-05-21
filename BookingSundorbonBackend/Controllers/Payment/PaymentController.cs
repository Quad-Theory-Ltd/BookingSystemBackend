using BookingSundorbon.Features.Repositories.PaymentRepository;
using BookingSundorbon.Features.Services.EmailService;
using BookingSundorbon.Views.DTOs.PaymentView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;

namespace BookingSundorbonBackend.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentRepository _paymentRepository;
        private readonly IConfiguration configuration;
        private readonly IEmailServices _emailServices;

        public PaymentController(IPaymentRepository paymentRepository, IConfiguration configuration, IEmailServices emailServices)
        {
            _paymentRepository = paymentRepository;
            this.configuration = configuration;
            _emailServices = emailServices;
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
            var parcelExist = await _paymentRepository.GetPaymentAsyncByParcelNoAsync(payment.ParcelOderNo);

            if (parcelExist != null)
            {
                return Ok("Already Exits!");
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

        [HttpPut("UpdatePaymentForStripePayment/{parcelId}")]
        public async Task<IActionResult> UpdatePaymentForStripePayment(int parcelId, [FromBody] StripePaymentDetails payment)
        {
            if (payment == null)
            {
                return BadRequest(" Payment Id is Invalid!");
            }
            var existingPayment = await _paymentRepository.GetPaymentAsyncByParcelNoAsync(parcelId);
            if (existingPayment == null)
            {
                return BadRequest(" Payment Not Found!");
            }
            existingPayment.CardBrand = payment.CardBrand;
            existingPayment.CardLast4 = payment.CardLast4;
            existingPayment.StripePaymentMethod = payment.StripePaymentMethod;
            existingPayment.StripePaymentIntentId = payment.StripePaymentIntentId;
            existingPayment.PaymentDate = DateTime.Now;
            existingPayment.PaymentMethodId = 6;
            existingPayment.PaymentStatusId = payment.PaymentStatusId;

            await _paymentRepository.UpdatePaymentAsync(existingPayment);
            return Ok();
        }

        //[HttpPut("UpdatePayment")]
        //public async Task<IActionResult> UpdatePayment([FromBody] PaymentView payment)
        //{
        //    if (payment == null)
        //    {
        //        return BadRequest(" Payment is Invalid!");
        //    }

        //    await _paymentRepository.UpdatePaymentAsync(payment);
        //    return NoContent();
        //}

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


        [HttpGet("GetAgentPayments/{userId}")]

        public async Task<IActionResult> GetAgentPayments(string userId)
        {
            var payment = await _paymentRepository.GetAgentPaymentsAsync(userId);
            if (payment == null)
            {
                return NotFound("Payment not found.");
            }
            return Ok(payment);
        }


        [HttpGet("GetAllPaymentStatus")]
        public async Task<IActionResult> GetAllActivePaymentStatus()
        {
            var paymentstatus = await _paymentRepository.GetAllActivePaymentStatus();
            return Ok(paymentstatus);
        }
        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent(PaymentIntentView paymentIntent)
        {
            var parcel = await _paymentRepository.GetPaymentAsyncByParcelNoAsync(paymentIntent.parcelId);

            if (parcel == null) return NotFound("Parcel not found");

            var amountInCents = (long)(parcel.OrderAmount * 100);

            var options = new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = paymentIntent.currency,
                Metadata = new Dictionary<string, string>
            {
                { "parcel_id", parcel.Id.ToString() },
                { "record_serial_no", parcel.RecordSerialNo }
            },
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                }
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);

            parcel.CreationDate = DateTime.Now;
            parcel.OrderAmount = parcel.OrderAmount;
            parcel.PaymentStatusId = 1;  // Pending
            parcel.StripePaymentIntentId = intent.Id;

            await _paymentRepository.UpdatePaymentAsync(parcel);

            return Ok(new
            {
                clientSecret = intent.ClientSecret,
                publishableKey = configuration["Stripe:PublishableKey"]
            });
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var sigHeader = Request.Headers["Stripe-Signature"];
            var secret = configuration["Stripe:WebhookSecret"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, sigHeader, secret);

                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    var intent = stripeEvent.Data.Object as PaymentIntent;
                    var payment = await _paymentRepository.GetPaymentAsyncByIntentIdAsync(intent.Id);

                    if (payment != null)
                    {
                        var charge = await new ChargeService().GetAsync(intent.LatestChargeId);

                        payment.PaymentStatusId = 2;  // Paid
                        payment.PaymentDate = DateTime.UtcNow;
                        payment.StripeChargeId = intent.LatestChargeId;
                        payment.CardLast4 = charge.PaymentMethodDetails.Card.Last4;
                        payment.CardBrand = charge.PaymentMethodDetails.Card.Brand;
                        payment.StripePaymentMethod = charge.PaymentMethodDetails.Type;
                        payment.ModificationDate = DateTime.UtcNow;

                        await _paymentRepository.UpdatePaymentAsync(payment);
                    }
                }

                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    var intent = stripeEvent.Data.Object as PaymentIntent;

                    var payment = await _paymentRepository.GetPaymentAsyncByIntentIdAsync(intent.Id);

                    if (payment != null)
                    {
                        payment.PaymentStatusId = 3;  // Decline
                        payment.PaymentDate = DateTime.UtcNow;
                        payment.ModificationDate = DateTime.UtcNow;

                        await _paymentRepository.UpdatePaymentAsync(payment);
                    }
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("UploadPaymentSlip/{parcelId}/{userEmail}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPaymentSlip(
                IFormFile file,
                [FromForm] int parcelId,
                [FromForm] string userEmail,
                [FromForm] string recordSerialNo)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            // ✅ Read file into memory (NO saving)
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            // Example: Save file
            //var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            //if (!Directory.Exists(uploadsFolder))
            //    Directory.CreateDirectory(uploadsFolder);

            //var safeFileName = $"{Guid.NewGuid()}_{file.FileName}";
            //var filePath = Path.Combine(uploadsFolder, safeFileName);

            //await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

            // Email subject/body
            string subject = $"Payment Slip - Parcel #{parcelId}";

            string body = $@" <!DOCTYPE html> <html> <head> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /> <meta name='viewport' content='width=device-width, initial-scale=1.0'/> </head> <body style='margin:0; padding:0; background-color:#f4f4f4; font-family:Arial,sans-serif;'> <table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#f4f4f4'> <tr> <td align='center' style='padding:40px 15px;'> <!-- Main Container --> <table width='620' border='0' cellspacing='0' cellpadding='0' style='background-color:#ffffff; border-radius:12px; overflow:hidden; box-shadow:0 2px 10px rgba(0,0,0,0.08);'> <!-- Header --> <tr> <td bgcolor='#000000' style='padding:28px 35px; color:#ff6600; font-size:30px; font-weight:bold; letter-spacing:0.5px;'> Sundarban Cargo </td> </tr> <!-- Orange Divider --> <tr> <td height='5' bgcolor='#ff6600'></td> </tr> <!-- Content --> <tr> <td style='padding:45px 35px; color:#333333; font-size:15px; line-height:26px;'> <!-- Title --> <div style='font-size:28px; font-weight:bold; color:#111111; margin-bottom:18px;'> Order Confirmed </div> <!-- Intro --> <div style='margin-bottom:28px; color:#555555;'> Thank you for choosing <strong>Sundarban Cargo Service</strong>. <br/><br/> Your order has been placed successfully. Please find your payment slip / invoice attached with this email for your records and future reference. </div> <!-- Info Box --> <table width='100%' border='0' cellspacing='0' cellpadding='0' style='border:1px solid #e6e6e6; border-radius:10px; overflow:hidden; background-color:#fafafa; margin-top:10px;'> <tr> <td style='padding:16px; width:220px; font-weight:bold; border-bottom:1px solid #e6e6e6; color:#222222;'> Parcel ID </td> <td style='padding:16px; border-bottom:1px solid #e6e6e6; color:#555555;'> #{parcelId} </td> </tr> <tr> <td style='padding:16px; width:220px; font-weight:bold; color:#222222;'> Record Serial No </td> <td style='padding:16px; color:#555555;'> {recordSerialNo} </td> </tr> </table> <!-- Note --> <div style='margin-top:35px; padding:18px; background-color:#fff7f0; border-left:4px solid #ff6600; color:#666666; font-size:14px; line-height:24px; border-radius:6px;'> Please keep the attached payment slip/invoice for verification and future communication regarding your shipment. </div> <!-- Footer Text --> <div style='margin-top:40px; color:#777777; font-size:14px;'> If you have any questions, feel free to contact our support team. <br/><br/> Regards,<br/> <strong>Sundarban Cargo Service</strong> </div> </td> </tr> <!-- Footer --> <tr> <td bgcolor='#f7f7f7' align='center' style='padding:22px; color:#999999; font-size:12px; border-top:1px solid #eeeeee;'> © Sundarban Cargo Service. All rights reserved. </td> </tr> </table> </td> </tr> </table> </body> </html>";


            // ✅ Send email with attachment
            var result = await _emailServices.SendMailAsync(
                userEmail,
                subject,
                body,
                new List<(byte[], string, string)>
                {
            (fileBytes, file.FileName, file.ContentType)
                }
            );

            return Ok(new
            {
                Message = "File sent successfully via email",
                ParcelId = parcelId,
                RecordSerialNo = recordSerialNo,
                EmailStatus = result
            });
        }

    }
}
