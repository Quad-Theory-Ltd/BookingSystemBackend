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
        public async Task<IActionResult> CreateRoutingType([FromBody] RouteView routeType)
        {
            if (routeType == null)
            {
                return BadRequest();
            }

            var newRouteId = await _routeRepository.CreateRouteTypeAsync(routeType);
            return CreatedAtAction(nameof(GetRoutingType), new { id = newRouteId }, newRouteId);

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetRoutingType(int id)
        {
            var routingType = await _routeRepository.GetRouteAsync(id);
            if (routingType == null)
            {
                return NotFound(" Routing Type not found.");
            }
            return Ok(routingType);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoutingType(int id, [FromBody] RouteView routeType)
        {
            if (routeType == null || routeType.Id != id)
            {
                return BadRequest("Measurement Data is Invalid!");
            }
            var existingRoutingType = await _routeRepository.GetRouteAsync(id);
            if (existingRoutingType == null)
            {
                return BadRequest(" Routing Type Not Found!");
            }
            await _routeRepository.UpdateRouteAsync(routeType);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoutingType(int id)
        {
            var routingType = await _routeRepository.GetRouteAsync(id);
            if (routingType == null)
            {
                return NotFound(" Routing Type not found.");
            }

            await _routeRepository.DeleteRouteAsync(id);
            return NoContent();
        }

    }
}
