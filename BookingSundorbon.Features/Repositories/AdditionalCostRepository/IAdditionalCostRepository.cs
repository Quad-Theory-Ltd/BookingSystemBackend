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
        Task<int> CreateAdditionalCostAsync(AdditionalCostView additionalCost);
        Task<AdditionalCostView> GetAdditionalCostAsync(int id);
        Task<IEnumerable<AdditionalCostView>> GetAllActiveAdditionalCostAsync();
        Task UpdateAdditionalCostAsync(AdditionalCostView additionalCost);
        Task DeleteAdditionalCostAsync(int id);
    }
}
