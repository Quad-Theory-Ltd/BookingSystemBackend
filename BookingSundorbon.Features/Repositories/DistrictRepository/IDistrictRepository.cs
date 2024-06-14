using BookingSundorbon.Views.DTOs.ActiveDistrictView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DistrictRepository
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<ActiveDistrictView>> GetAllDistrictsAsync();
    }
}
