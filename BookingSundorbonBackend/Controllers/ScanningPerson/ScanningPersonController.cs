using BookingSundorbon.Features.Repositories.ScanningPersonRepository;
using BookingSundorbon.Views.DTOs.ScanningPersonView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ScanningPerson
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanningPersonController : ControllerBase
    {

        private readonly IScanningPersonRepository _scanningPersonRepository;

        public ScanningPersonController(IScanningPersonRepository scanningPersonRepository)
        {
            _scanningPersonRepository = scanningPersonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveScanningPersons()
        {
            var scanningPerson = await _scanningPersonRepository.GetAllActiveScanningPersonsAsync();
            return Ok(scanningPerson);
        }


        [HttpPost]
        public async Task<IActionResult> CreateScanningPerson([FromBody] ScanningPersonView scanningPerson)
        {
            if (scanningPerson == null)
            {
                return BadRequest("ScanningPerson is Null");
            }
            var scanningPersonId = await _scanningPersonRepository.CreateScanningPersonAsync(scanningPerson);

            return CreatedAtAction(nameof(GetScanningPerson), new { id = scanningPersonId }, scanningPersonId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetScanningPerson(int id)
        {
            var scanningPerson = await _scanningPersonRepository.GetScanningPersonAsync(id);
            if (scanningPerson == null)
            {
                return NotFound("ScanningPerson not found.");
            }
            return Ok(scanningPerson);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScanningPerson(int id, [FromBody] ScanningPersonView scanningPerson)
        {
            if (scanningPerson == null || scanningPerson.Id != id)
            {
                return BadRequest("ScanningPerson Id is Invalid!");
            }
            var existingScanningPerson = await _scanningPersonRepository.GetScanningPersonAsync(id);
            if (existingScanningPerson == null)
            {
                return BadRequest("ScanningPerson Not Found!");
            }
            await _scanningPersonRepository.UpdateScanningPersonAsync(scanningPerson);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScanningPerson(int id)
        {
            var scanningPerson = await _scanningPersonRepository.GetScanningPersonAsync(id);
            if (scanningPerson == null)
            {
                return NotFound("ScanningPerson not found.");
            }

            await _scanningPersonRepository.DeleteScanningPersonAsync(id);
            return NoContent();
        }

    }
}
