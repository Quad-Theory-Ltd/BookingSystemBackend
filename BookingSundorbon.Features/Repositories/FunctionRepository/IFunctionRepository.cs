using BookingSundorbon.Views.DTOs.FunctionView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.FunctionRepository
{
    public interface IFunctionRepository
    {
        Task CreateFunctionAsync(FunctionView function);
        Task<FunctionView> GetFunctionAsync(string id);
        Task<IEnumerable<FunctionView>> GetAllActiveFunctionAsync();
        Task UpdateFunctionAsync(FunctionView function);
        Task DeleteFunctionAsync(string id);
    }
}
