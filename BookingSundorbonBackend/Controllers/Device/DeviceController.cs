using BookingSundorbon.Features.Repositories.DeviceRepository;
using BookingSundorbon.Views.DTOs.DeviceView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IDeviceRepository _deviceRepository;

        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveDevices()
        {
            var device = await _deviceRepository.GetAllActiveDevicesAsync();
            return Ok(device);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromBody] DeviceView device)
        {
            if (device == null)
            {
                return BadRequest("Device is Null");
            }
            var deviceId = await _deviceRepository.CreateDeviceAsync(device);

            return CreatedAtAction(nameof(GetDevice), new { id = deviceId }, deviceId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _deviceRepository.GetDeviceAsync(id);
            if (device == null)
            {
                return NotFound("Device not found.");
            }
            return Ok(device);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] DeviceView device)
        {
            if (device == null || device.Id != id)
            {
                return BadRequest(" Device Id is Invalid!");
            }
            var existingDevice = await _deviceRepository.GetDeviceAsync(id);
            if (existingDevice == null)
            {
                return BadRequest(" Device Not Found!");
            }
            await _deviceRepository.UpdateDeviceAsync(device);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _deviceRepository.GetDeviceAsync(id);
            if (device == null)
            {
                return NotFound("Device not found.");
            }

            await _deviceRepository.DeleteDeviceAsync(id);
            return NoContent();
        }

    }
}
