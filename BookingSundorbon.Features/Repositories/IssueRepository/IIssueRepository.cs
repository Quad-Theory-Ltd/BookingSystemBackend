using BookingSundorbon.Views.DTOs.IssueView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.IssueRepository
{
    public interface IIssueRepository
    {
        Task<int> CreateIssueAsync(IssueView issue);
        Task<IssueView> GetIssueAsync(int issueNo);
        Task<IEnumerable<IssueView>> GetAllIssueAsync();
        //Task UpdateIssueAsync(IssueView issue);
        //Task DeleteIssueAsync(int id);
    }
}
