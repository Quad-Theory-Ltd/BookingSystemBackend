using BookingSundorbon.Features.Repositories.ApplicationUserRepository;
using BookingSundorbon.Features.Repositories.GetTransitionCostRepository;
using BookingSundorbon.Features.Repositories.NotificationRepository;
using BookingSundorbon.Features.Repositories.ParcelStatusRepository;
using BookingSundorbon.Features.Repositories.RouteRepository;
using BookingSundorbon.Views.DTOs.GetTransitionCostView;
using BookingSundorbon.Views.DTOs.NotificationView;
using BookingSundorbon.Views.DTOs.TransitionCostView;
using BookingSundorbonBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.GetTransitionCost
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransitionCostController : ControllerBase
    {
        private readonly ITransitionCostRepository _getTransitionCostRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly INotificationHubService _notificationHubService;
        private readonly IParcelStatusRepository _parcelStatusRepository;
        private readonly IRouteRepository _routeRepository;

        public TransitionCostController(
            ITransitionCostRepository getTransitionCostRepository,
            INotificationRepository notificationRepository,
            IApplicationUserRepository applicationUserRepository,
            INotificationHubService notificationHubService,
            IParcelStatusRepository parcelStatusRepository,
            IRouteRepository routeRepository)
        {
            _getTransitionCostRepository = getTransitionCostRepository;
            _notificationRepository = notificationRepository;
            _applicationUserRepository = applicationUserRepository;
            _notificationHubService = notificationHubService;
            _parcelStatusRepository = parcelStatusRepository;
            _routeRepository = routeRepository;
        }

        [HttpPost]
        [Route("GetTotalTransitionCost")]
        public async Task<IActionResult> GetTotalTransitionCost([FromBody] List<GetTransitionCostView> getTransitionCost)
        {
            try
            {
                if (getTransitionCost == null || !getTransitionCost.Any())
                    return BadRequest("Request list is empty!");

                var result = await _getTransitionCostRepository.GetTransitionCost(getTransitionCost);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateParcelBooking([FromBody] List<CreateParcelBookingView> createParcelBookingViews)
        {
            if (createParcelBookingViews == null)
            {
                return BadRequest("Parcel Booking is null.");
            }

            var result = await _getTransitionCostRepository.CreateParcelBookingAsync(createParcelBookingViews);

            await NotifyAdminsOfParcelBookingAsync(createParcelBookingViews, result);

            //await _notificationHubService.SendToGroupAsync("ParcelBookingCreated", "NewBookingCreated", result);

            return Ok(result);
        }

        private async Task NotifyAdminsOfParcelBookingAsync(
            List<CreateParcelBookingView> bookings,
            List<CreateParcelBookingOutputView> bookingResults)
        {
            var adminUsers = (await _applicationUserRepository.GetAdminUserDetailsAsync()).ToList();
            var adminUserIds = adminUsers.Select(a => a.Id).ToList();
            if (adminUserIds.Count == 0 || bookingResults.Count == 0)
            {
                return;
            }

            

            var firstResult = bookingResults[0];
            var firstBooking = bookings[0];
            var reference = !string.IsNullOrWhiteSpace(firstResult.RecordSerialNo)
                ? firstResult.RecordSerialNo
                : firstResult.Barcode;

            long? createdBy = null;
            if (long.TryParse(firstBooking.CreatorId, out var creatorId))
            {
                createdBy = creatorId;
            }
            else if (long.TryParse(firstBooking.BookedById, out var bookedById))
            {
                createdBy = bookedById;
            }


            var parcelStatus = await _parcelStatusRepository.GetAllActiveParcelStatusByRouteId(bookings.FirstOrDefault().RoutingTypeId);

            if (parcelStatus.Count() == 0)
            {
                var routeDetails = await _routeRepository.GetRouteAsync(bookings.FirstOrDefault().RoutingTypeId);
                var notificationRequestForparcelStatus = new CreateNotificationView
                {
                    Title = "Parcel Status Configuration Required",
                    Message = $"No parcel statuses are configured for route '{routeDetails.RouteName}'. Please set up the required parcel statuses before processing parcels on this route.",
                    NotificationType = "ParcelStatusSetup",
                    ReferenceId = routeDetails.Id,
                    ReferenceType = "Route",
                    RedirectUrl = "/admin-dashboard/setup/parcel-status",
                    CreatedBy = createdBy,
                    RecipientUserIds = adminUserIds
                };

                var notificationForParcelStatusId = await _notificationRepository.CreateNotificationAsync(notificationRequestForparcelStatus);

            }

            var notificationRequest = new CreateNotificationView
            {
                Title = "New parcel booking",
                Message = $"New parcel booking created · Ref: {reference} · #{firstResult.ParcelId}",
                NotificationType = "ParcelBooking",
                ReferenceId = firstResult.ParcelId,
                ReferenceType = "Parcel",
                RedirectUrl = $"/admin-dashboard/bookings",
                CreatedBy = createdBy,
                RecipientUserIds = adminUserIds
            };

            var notificationId = await _notificationRepository.CreateNotificationAsync(notificationRequest);

            foreach (var userId in adminUserIds)
            {
                var payload = new UserNotificationView
                {
                    NotificationId = notificationId,
                    Title = notificationRequest.Title,
                    Message = notificationRequest.Message,
                    NotificationType = notificationRequest.NotificationType,
                    ReferenceId = notificationRequest.ReferenceId,
                    ReferenceType = notificationRequest.ReferenceType,
                    RedirectUrl = notificationRequest.RedirectUrl,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = notificationRequest.CreatedBy,
                    IsRead = false
                };

                await _notificationHubService.SendToGroupAsync(
                    $"ParcelBookingCreated_" + userId,
                    "NewBookingCreated",
                    payload);
            }
        }
    }
}
