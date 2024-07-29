using BookingSundorbon.Views.DTOs.ScreenFunctionView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ScreenFunctionRepository
{
    public interface IScreenFunctionRepository
    {
        Task CreateScreenFunctionAsync(ScreenFunctionView screen);
        Task<ScreenFunctionView> GetScreenFunctionAsync(string id);
        Task<IEnumerable<ScreenFunctionView>> GetAllActiveScreenesFunctionAsync();
        Task UpdateScreenFunctionAsync(ScreenFunctionView screen);
        Task DeleteScreenFunctionAsync(string id);
    }
}
