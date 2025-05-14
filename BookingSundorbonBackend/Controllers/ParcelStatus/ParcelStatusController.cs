using BookingSundorbon.Features.Repositories.ParcelStatusRepository;
using BookingSundorbon.Views.DTOs.ParcelStatusView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ParcelStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelStatusController : ControllerBase
    {

        private readonly IParcelStatusRepository _parcelStatusRepository;

        public ParcelStatusController(IParcelStatusRepository parcelStatusRepository)
        {
            _parcelStatusRepository = parcelStatusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveParcelStatuss()
        {
            var parcelStatus = await _parcelStatusRepository.GetAllActiveParcelStatusAsync();
            return Ok(parcelStatus);
        }


        [HttpPost]
        public async Task<IActionResult> CreateParcelStatus([FromBody] ParcelStatusView parcelStatus)
        {
            if (parcelStatus == null)
            {
                return BadRequest("ParcelStatus is Null");
            }
            var parcelStatusId = await _parcelStatusRepository.CreateParcelStatusAsync(parcelStatus);

            return CreatedAtAction(nameof(GetParcelStatus), new { id = parcelStatusId }, parcelStatusId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetParcelStatus(int id)
        {
            var parcelStatus = await _parcelStatusRepository.GetParcelStatusAsync(id);
            if (parcelStatus == null)
            {
                return NotFound("ParcelStatus not found.");
            }
            return Ok(parcelStatus);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParcelStatus(int id, [FromBody] ParcelStatusView parcelStatus)
        {
            if (parcelStatus == null || parcelStatus.Id != id)
            {
                return BadRequest(" ParcelStatus Id is Invalid!");
            }
            var existingParcelStatus = await _parcelStatusRepository.GetParcelStatusAsync(id);
            if (existingParcelStatus == null)
            {
                return BadRequest(" ParcelStatus Not Found!");
            }
            await _parcelStatusRepository.UpdateParcelStatusAsync(parcelStatus);
            return NoContent();
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteParcelStatus(int id)
        //{
        //    var parcelStatus = await _parcelStatusRepository.GetParcelStatusAsync(id);
        //    if (parcelStatus == null)
        //    {
        //        return NotFound("ParcelStatus not found.");
        //    }

        //    await _parcelStatusRepository.DeleteParcelStatusAsync(id);
        //    return NoContent();
        //}

    }
}
