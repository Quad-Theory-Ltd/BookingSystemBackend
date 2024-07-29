using BookingSundorbon.Views.DTOs.CountryView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CountryRepository
{
    public interface ICountryRepository
    {
        
        Task<int> CreateCountryAsync(ActiveCountryView country);
        Task<ActiveCountryView> GetCountryAsync(int id);
        Task<IEnumerable<ActiveCountryView>> GetAllActiveCountriesAsync();
        Task UpdateCountryAsync(ActiveCountryView country);
        Task DeleteCountryAsync(int id);
    }
}
