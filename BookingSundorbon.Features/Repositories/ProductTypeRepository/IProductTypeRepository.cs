using BookingSundorbon.Views.DTOs.ActiveProductTypeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ProductTypeRepository
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ActiveProductTypeView>> GetAllActiveProductTypesAsync();
    }
}
