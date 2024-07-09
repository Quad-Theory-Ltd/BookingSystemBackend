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
        Task CreateRoleAsynce(CreateRoleView role);
    }
}
