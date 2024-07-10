using BookingSundorbon.Views.DTOs.RoleView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        Task CreateRoleAsynce(RoleView role);
        Task<RoleView> GetRoleAsync(string id);
        Task<IEnumerable<RoleView>> GetAllActiveRolesAsync();
        Task UpdateRoleAsync(RoleView role);
        Task DeleteRoleAsync(string id);
    }
}
