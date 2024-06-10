using BookingSundorbon.Views.DTOs.CompanyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CompanyRepository
{
    public interface ICompanyRepository
    {
        Task<int> CreateCompanyAsync(CompanyView company);
        Task<CompanyView> GetCompanyAsync(int id);
        Task<IEnumerable<CompanyView>> GetAllCompaniesAsync();
        Task UpdateCompanyAsync(CompanyView company);
        Task DeleteCompanyAsync(int id);
    }
}
