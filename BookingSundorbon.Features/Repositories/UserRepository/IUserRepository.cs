using BookingSundorbon.Views.DTOs.S_UserView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<EmployeeView>> GetAllEmployeeAsync();
    }
}
