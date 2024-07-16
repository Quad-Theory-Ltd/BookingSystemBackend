using BookingSundorbon.Views.DTOs.ExtraPackagingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ExtraPackagingRepository
{
    public interface IExtraPackagingRepository
    {
        Task<int> CreateExtraPackagingAsync(ExtraPackagingView extraPackaging);
        Task<ExtraPackagingView> GetExtraPackagingAsync(int id);
        Task<IEnumerable<ExtraPackagingView>> GetAllActiveExtraPackagingsAsync();
        Task UpdateExtraPackagingAsync(ExtraPackagingView extraPackaging);
        Task DeleteExtraPackagingAsync(int id);
    }
}
