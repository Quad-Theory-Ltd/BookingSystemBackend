using BookingSundorbon.Views.DTOs.DepartmentalOperationView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DepartmentalOperationRepository
{
    public interface IDepartmentalOperationRepository
    {
        Task<int> CreateDepartmentalOperationAsync(DepartmentalOperationView departmentalOperation);
        Task<DepartmentalOperationView> GetDepartmentalOperationAsync(int id);
        Task<IEnumerable<DepartmentalOperationView>> GetAllDepartmentalOperationAsync();
        Task UpdateDepartmentalOperationAsync(DepartmentalOperationView departmentalOperation);
        Task DeleteDepartmentalOperationAsync(int id);
    }
}
