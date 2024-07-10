using BookingSundorbon.Views.DTOs.ScreenView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ScreenRepository
{
    public interface IScreenRepository
    {
        Task CreateScreenAsync(ScreenView screen);
        Task<ScreenView> GetScreenAsync(string id);
        Task<IEnumerable<ScreenView>> GetAllActiveScreenesAsync();
        Task UpdateScreenAsync(ScreenView screen);
        Task DeleteScreenAsync(string id);
    }
}
