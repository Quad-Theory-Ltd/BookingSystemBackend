using BookingSundorbon.Views.DTOs.PickupView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.PickupRepository
{
   public interface IPickupRepository
    {
        Task<int> CreatePickupAsync(PickupView pickup);
        Task<PickupView> GetPickupAsync(int id);
        Task<IEnumerable<PickupView>> GetAllActivePickupUnitsAsync();
        Task UpdatePickupAsync(PickupView pickup);
        Task DeletePickupAsync(int id);
    }
}
