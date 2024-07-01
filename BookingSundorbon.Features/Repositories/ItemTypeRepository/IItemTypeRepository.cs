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
    }
}
