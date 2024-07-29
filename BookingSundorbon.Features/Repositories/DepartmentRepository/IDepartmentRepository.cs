using BookingSundorbon.Views.DTOs.DepartmentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<int> CreateDepartmentAsync(DepartmentView department);
        Task<DepartmentView> GetDepartmentAsync(int id);
        Task<IEnumerable<DepartmentView>> GetAllDepartmentsAsync();
        Task UpdateDepartmentAsync(DepartmentView department);
        Task DeleteDepartmentAsync(int id);
    }
}
