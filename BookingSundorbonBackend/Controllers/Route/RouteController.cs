using BookingSundorbon.Features.Repositories.BranchRepository;
using BookingSundorbon.Features.Repositories.RouteRepository;
using BookingSundorbon.Views.DTOs.RouteView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Route
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {

        private readonly IRouteRepository _routeRepository;

        public RouteController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActiveRoutes()
        {
            var routes = await _routeRepository.GetAllActiveRoutesAsync();
            return Ok(routes);
        }

        [HttpPost]
        public async Task <IActionResult> InsertIntoRoutingType([FromBody] CreateRouteTypeView routeType)
        {
            if (routeType == null)
            {
                return BadRequest();
            }

            var newRouteId = await _routeRepository.CreateRouteTypeAsync(routeType);
            // return CreatedAtAction(nameof(), new { id = newRouteId }, newRouteId);
            return Ok(newRouteId);
        }

    }
}
