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
        Task<IEnumerable<ActiveRouteView>> GetAllActiveRoutesAsync();
        Task <int> CreateRouteTypeAsync(CreateRouteTypeView routeType);
    }

}
