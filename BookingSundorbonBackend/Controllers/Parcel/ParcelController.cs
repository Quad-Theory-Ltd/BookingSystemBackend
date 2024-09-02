using BookingSundorbon.Features.Repositories.ParcelRepository;
using BookingSundorbon.Views.DTOs.ParcelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Parcel
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {

        private readonly IParcelRepository _parcelRepository;

        public ParcelController(IParcelRepository parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }


        [HttpGet("GetParcelInfoById/{id}")]
        public async Task<IActionResult> GetParcelInfoById(int id)
        {
            var parcel = await _parcelRepository.GetParcelInfoByIdAsync(id);
            if (parcel == null)
            {
                return NotFound("Parcel not found.");
            }
            return Ok(parcel);
        }

        [HttpGet("GetAllParcelNo")]

        public async Task<IActionResult> GetAllParcelNo()
        {
            var parcel = await _parcelRepository.GetAllParcelNoAsync();
            if (parcel == null)
            {
                return NotFound("Parcel No not found.");
            }
            return Ok(parcel);
        }

        [HttpGet("GetAllScanningPerson")]

        public async Task<IActionResult> GetAllScanningPerson()
        {
            var person = await _parcelRepository.GetAllScanningPersonsAsync();
            if (person == null)
            {
                return NotFound("Scanning Person not found.");
            }
            return Ok(person);
        }

        [HttpGet("GetLastParcelRecordSerialNo")]
        public async Task<IActionResult> GetLastParcelRecordSerialNo()
        {
            var slNo = await _parcelRepository.GetLastParcelRecordSerialNoAsync();
            if (slNo == null)
            {
                return NotFound("RecordSerialNo not found");
            }
            return Ok(slNo);
        }



    }
}
