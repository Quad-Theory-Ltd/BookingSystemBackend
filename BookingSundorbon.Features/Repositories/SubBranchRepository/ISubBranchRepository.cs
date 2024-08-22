using BookingSundorbon.Views.DTOs.SubBranchView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.SubBranchRepository
{
    public interface ISubBranchRepository
    {
        Task<int> CreateSubBranchAsync(SubBranchCreateView subBranch);
        Task<SubBranchView> GetSubBranchAsync(int id);
        Task<IEnumerable<SubBranchView>> GetAllActiveSubBranchsAsync();
        Task UpdateSubBranchAsync(SubBranchView subBranch);
        Task DeleteSubBranchAsync(int id);
    }
}
