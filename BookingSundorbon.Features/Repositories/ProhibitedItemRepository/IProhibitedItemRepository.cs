using BookingSundorbon.Views.DTOs.ActiveProductTypeView;
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
        Task<IEnumerable<ProhibitedItemView>> GetAllActiveProhitedItemsAsync();
        Task<int> CreateProhibitedItemAsync(CreateProhibitedItemView prohibitedItem);
    }
}
