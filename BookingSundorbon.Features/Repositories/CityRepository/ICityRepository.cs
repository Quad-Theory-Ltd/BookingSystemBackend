using BookingSundorbon.Views.DTOs.CityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CityRepository
{
    public interface ICityRepository
    {
        Task<List<ActiveCityView>> GetAllCitiesAsync();
        Task<ActiveCityView> GetCityByIdAsync(int id, bool isActive);
    }
}
