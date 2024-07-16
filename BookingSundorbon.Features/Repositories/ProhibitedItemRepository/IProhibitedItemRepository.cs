using BookingSundorbon.Views.DTOs.ProhibitedItemView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ProhibitedItemRepository
{
    public interface IProhibitedItemRepository
    {
        Task<IEnumerable<ProhibitedItemView>> GetAllActiveProhibitedItemsAsync();
        Task<int> CreateProhibitedItemAsync(ProhibitedItemView prohibitedItem);
        Task<ProhibitedItemView> GetProhibitedItemAsync(int id);
        Task UpdateProhibitedItemAsync(ProhibitedItemView prohibiteditem);
        Task DeleteProhibitedItemAsync(int id);
    }
}
