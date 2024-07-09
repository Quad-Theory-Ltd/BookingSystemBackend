using BookingSundorbon.Features.Repositories.VATConfigurationRepository;
using BookingSundorbon.Views.DTOs.VATConfigurationView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.VATConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class VATConfigurationController : ControllerBase
    {
        private readonly IVATConfigurationRepository _vatConfigurationRepository;

        public VATConfigurationController(IVATConfigurationRepository vatConfigurationRepository)
        {
            _vatConfigurationRepository = vatConfigurationRepository;
        }

        [HttpPost]
        public async Task <IActionResult> CreateVatConfiguration([FromBody] CreateVATConfigurationView vatConfiguration)
        {
            if(vatConfiguration == null)
            {
                return BadRequest("VAT configuration null");
            }

            var vatConfigurationId = await _vatConfigurationRepository.CreateVatConfigurationAsync(vatConfiguration);
            
            return Ok(vatConfigurationId);

        }
        

    }
}
