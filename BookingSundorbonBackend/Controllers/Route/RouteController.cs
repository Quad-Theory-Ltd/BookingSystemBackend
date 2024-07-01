using BookingSundorbon.Features.Repositories.RouteRepository;
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

    }
}
