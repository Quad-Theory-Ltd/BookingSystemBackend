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

        [HttpGet]
        public async Task<IActionResult> GetAllActiveVATConfiguration()
        {
            var vatconfiguration = await _vatConfigurationRepository.GetAllActiveVATConfigurationesAsync();
            return Ok(vatconfiguration);
        }

        [HttpPost]
        public async Task <IActionResult> CreateVatConfiguration([FromBody] VATConfigurationView vatConfiguration)
        {
            if(vatConfiguration == null)
            {
                return BadRequest("VAT configuration null");
            }

            var vatConfigurationId = await _vatConfigurationRepository.CreateVatConfigurationAsync(vatConfiguration);
            
            return CreatedAtAction(nameof(GetVATConfiguration), new {id = vatConfigurationId}, vatConfigurationId);

        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetVATConfiguration(int id)
        {
            var vatConfiguration = await _vatConfigurationRepository.GetVATConfigurationAsync(id);
            if (vatConfiguration == null)
            {
                return NotFound("Vat Configuration not found.");
            }
            return Ok(vatConfiguration);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVATConfiguration(int id, [FromBody] VATConfigurationView vatConfiguration)
        {
            if (vatConfiguration == null || vatConfiguration.Id != id)
            {
                return BadRequest("Vat Configuration Id is Invalid!");
            }
            var existingVATConfiguration = await _vatConfigurationRepository.GetVATConfigurationAsync(id);
            if (existingVATConfiguration == null)
            {
                return BadRequest(" Vat Configuration Not Found!");
            }
            await _vatConfigurationRepository.UpdateVATConfigurationAsync(vatConfiguration);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVATConfiguration(int id)
        {
            var vatConfiguration = await _vatConfigurationRepository.GetVATConfigurationAsync(id);
            if (vatConfiguration == null)
            {
                return NotFound("Vat Configuration not found.");
            }

            await _vatConfigurationRepository.DeleteVATConfigurationAsync(id);
            return NoContent();
        }


    }
}
