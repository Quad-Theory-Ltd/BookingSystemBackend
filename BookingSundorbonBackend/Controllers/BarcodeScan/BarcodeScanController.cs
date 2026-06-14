using BookingSundorbon.Features.Repositories.BarcodeScanRepository;
using BookingSundorbon.Features.Repositories.NotificationRepository;
using BookingSundorbon.Views.DTOs.BarcodeScanView;
using BookingSundorbon.Views.DTOs.NotificationView;
using BookingSundorbonBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;

namespace BookingSundorbonBackend.Controllers.BarcodeScan
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeScanController : ControllerBase
    {

        private readonly IBarcodeScanRepository _barcodeScanRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationHubService _notificationHubService;

        public BarcodeScanController(
            IBarcodeScanRepository barcodeScanRepository,
            INotificationRepository notificationRepository,
            INotificationHubService notificationHubService)
        {
            _barcodeScanRepository = barcodeScanRepository;
            _notificationRepository = notificationRepository;
            _notificationHubService = notificationHubService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveBarcodeScans()
        {
            var barcodeScan = await _barcodeScanRepository.GetAllActiveBarcodeScansAsync();
            return Ok(barcodeScan);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBarcodeScan([FromBody] BarcodeScanView barcodeScan)
        {
            if (barcodeScan == null)
            {
                return BadRequest("BarcodeScan is Null");
            }
            var barcodeScanId = await _barcodeScanRepository.CreateBarcodeScanAsync(barcodeScan);

            var createdBy = long.TryParse(barcodeScan.CreatorId, out var creatorIdLong) ? creatorIdLong : (long?)null;

            var notification = new CreateNotificationView
            {
                Title = "Parcel status update",
                Message = $"Parcel status updated to \"{barcodeScan.ParcelStatusName}\" · Parcel #{barcodeScan.ParcelNo}",
                NotificationType = "BarcodeScan",
                ReferenceId = barcodeScan.ParcelNo,
                ReferenceType = "Parcel",
                RedirectUrl = $"/",
                CreatedBy = createdBy,
                RecipientUserIds = new List<string> { barcodeScanId.BookedByUserId }
            };

            await _notificationRepository.CreateNotificationAsync(notification);

            var userId = Regex.Replace(barcodeScanId.BookedByUserId ?? string.Empty, @"\s+", "");

            await _notificationHubService.SendToGroupAsync("CustomerGroup_" + userId, "ParcelStatusUpdate", barcodeScan);

            return CreatedAtAction(nameof(GetBarcodeScan), new { id = barcodeScanId }, barcodeScanId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetBarcodeScan(int id)
        {
            var barcodeScan = await _barcodeScanRepository.GetBarcodeScanAsync(id);
            if (barcodeScan == null)
            {
                return NotFound("BarcodeScan not found.");
            }
            return Ok(barcodeScan);
        }

        [HttpGet("GetAgentBarcodeScan/{userId}")]
        public async Task<IActionResult> GetAgentBarcodeScan(string userId)
        {
            var barcodeScan = await _barcodeScanRepository.GetAgentBarcodeScanAsync(userId);
            if (barcodeScan == null)
            {

                return NotFound("BarcodeScan not found.");
            }
            return Ok(barcodeScan);
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateBarcodeScan(int id, [FromBody] BarcodeScanView barcodeScan)
        //{
        //    if (barcodeScan == null || barcodeScan.Id != id)
        //    {
        //        return BadRequest(" BarcodeScan Id is Invalid!");
        //    }
        //    var existingBarcodeScan = await _barcodeScanRepository.GetBarcodeScanAsync(id);
        //    if (existingBarcodeScan == null)
        //    {
        //        return BadRequest(" BarcodeScan Not Found!");
        //    }
        //    await _barcodeScanRepository.UpdateBarcodeScanAsync(barcodeScan);
        //    return NoContent();
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBarcodeScan(int id)
        //{
        //    var barcodeScan = await _barcodeScanRepository.GetBarcodeScanAsync(id);
        //    if (barcodeScan == null)
        //    {
        //        return NotFound("BarcodeScan not found.");
        //    }

        //    await _barcodeScanRepository.DeleteBarcodeScanAsync(id);
        //    return NoContent();
        //}

    }
}
