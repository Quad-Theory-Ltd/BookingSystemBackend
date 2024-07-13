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
        Task<int> CreateCityAsync(ActiveCityView city);
        Task<ActiveCityView> GetCityAsync(int id);
        Task<IEnumerable<ActiveCityView>> GetAllActiveCitiesAsync();
        Task UpdateCityAsync(ActiveCityView city);
        Task DeleteCityAsync(int id);
    }
}
