using BookingSundorbon.Views.DTOs.BranchView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BranchRepository
{
    public interface IBranchRepository
    {
        Task<int> CreateBranchAsync(BranchView branch);
        Task<BranchView> GetBranchAsync(int id);
        Task<IEnumerable<BranchView>> GetAllBranchesAsync();
        Task UpdateBranchAsync(BranchView branch);
        Task DeleteBranchAsync(int id);
    }
}
