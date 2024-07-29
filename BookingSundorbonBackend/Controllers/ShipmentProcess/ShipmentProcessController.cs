using BookingSundorbon.Features.Repositories.ProductTypeRepository;
using BookingSundorbon.Features.Repositories.ShipmentProcessRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.ShipmentProcess
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentProcessController : ControllerBase
    {
        private readonly IShipmentProcessRepository _shipmentProcessRepository;

        public ShipmentProcessController(IShipmentProcessRepository shipmentProcessRepository)
        {
            _shipmentProcessRepository = shipmentProcessRepository;
        }
       
    }
}
