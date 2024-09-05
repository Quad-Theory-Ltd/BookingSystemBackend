using BookingSundorbon.Views.DTOs.AgentBoxAssignView;
using BookingSundorbon.Views.DTOs.AgentBoxAssignView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AgentBoxAssignRepository
{
    public interface IAgentBoxAssignRepository
    {
        Task<int> CreateAgentBoxAssignAsync(AgentBoxAssignView agentBoxAssign);
        Task<AgentBoxAssignView> GetAgentBoxAssignAsync(int id);
        Task<IEnumerable<AgentBoxAssignView>> GetAllActiveAgentBoxAssignsAsync();
        Task UpdateAgentBoxAssignAsync(AgentBoxAssignView agentBoxAssign);
        //Task DeleteAgentBoxAssignAsync(int id);
        Task<IEnumerable<AgentBoxAssignDetailsView>> AgentBoxAssignDetailsByAgentIdAsync(int id);
        Task<AgentBoxAssignView> AgentBoxAssignByDetailsByIdAsync(int id);
       
    }
}
