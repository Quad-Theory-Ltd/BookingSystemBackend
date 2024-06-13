using BookingSundorbon.Views.DTOs.ActiveParcelContentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelContentRepository
{
    public interface IParcelContentRepository
    {
        Task<IEnumerable<ActiveParcelContentView>> GetAllActiveParcelContentsAsync();
    }
}
