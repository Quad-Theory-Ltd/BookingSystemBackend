using BookingSundorbon.Views.DTOs.RouteView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.RouteRepository
{
    public interface IRouteRepository
    {
        Task<IEnumerable<RouteView>> GetAllActiveRoutesAsync();
        Task <int> CreateRouteTypeAsync(RouteView routeType);
        Task<RouteView> GetRouteAsync(int id);
        Task UpdateRouteAsync(RouteView route);
        Task DeleteRouteAsync(int id);
    }

}
