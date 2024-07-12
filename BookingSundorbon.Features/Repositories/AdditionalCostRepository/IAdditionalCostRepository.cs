using BookingSundorbon.Views.DTOs.AdditionalCostView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AdditionalCostRepository
{
    public interface IAdditionalCostRepository
    {
        Task<int> CreateAdditionalCostAsync(AdditionalCostView AdditionalCost);
        Task<AdditionalCostView> GetAdditionalCostAsync(int id);
        Task<IEnumerable<AdditionalCostView>> GetAllActiveAdditionalCostAsync();
        Task UpdateAdditionalCostAsync(AdditionalCostView AdditionalCost);
        Task DeleteAdditionalCostAsync(int id);
    }
}
