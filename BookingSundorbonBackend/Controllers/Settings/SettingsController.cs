using BookingSundorbon.Features.Repositories.SettingsRepository;
using BookingSundorbon.Views.DTOs.SettingsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsController(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        [HttpGet("GetGtmSettings")]
        public async Task<IActionResult> GetGtmSettings()
        {
            try
            {
                var gtmSettings = await _settingsRepository.GetGtmSettingsAsync();
                return Ok(gtmSettings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving GTM settings", error = ex.Message });
            }
        }

        [HttpPost("SaveGtmSettings")]
        public async Task<IActionResult> SaveGtmSettings([FromBody] SaveGtmSettingsView settings)
        {
            try
            {
                if (settings == null)
                {
                    return BadRequest("Settings data is required");
                }

                await _settingsRepository.SaveGtmSettingsAsync(settings);
                return Ok(new { message = "GTM settings saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error saving GTM settings", error = ex.Message });
            }
        }
    }
}
