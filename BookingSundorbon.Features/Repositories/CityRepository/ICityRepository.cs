using BookingSundorbon.Views.DTOs.ActiveCityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CityRepository
{
    public interface ICityRepository
    {
        Task<IEnumerable<ActiveCityView>> GetAllActiveCitiesAsync();
    }
}
