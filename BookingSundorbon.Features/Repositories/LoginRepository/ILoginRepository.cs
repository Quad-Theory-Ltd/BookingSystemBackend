using BookingSundorbon.Views.DTOs.LoginView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.LoginRepository
{
    public interface ILoginRepository
    {
        //Task<LoginView> GetAllCustomerListAsync(string userName, string password,string userType);
        Task<LoginView> GetLoginByIdAsync(string userName, string password,string userType);
    }
}
