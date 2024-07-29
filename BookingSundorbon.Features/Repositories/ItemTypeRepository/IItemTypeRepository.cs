using BookingSundorbon.Views.DTOs.ItemTypeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ItemTypeRepository
{
    public interface IItemTypeRepository
    {
        Task<IEnumerable<ActiveItemTypeView>> GetAllActiveItemTypesAsync();
        Task<int> CreateItemTypeAsync(ActiveItemTypeView itemType);
        Task<ActiveItemTypeView> GetItemTypeAsync(int id);
        Task UpdateItemTypeAsync(ActiveItemTypeView itemType);
        Task DeleteItemTypeAsync(int id);
    }
}
