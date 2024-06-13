using BookingSundorbon.Views.DTOs.DistrictView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DistrictRepository
{
    public interface IDistrictRepository
    {
        Task<List<ActiveDistrictView>> GetAllDistrictsAsync();
        Task<ActiveDistrictView> GetDistrictByIdAsync(int id, bool isActive);
    }
}
