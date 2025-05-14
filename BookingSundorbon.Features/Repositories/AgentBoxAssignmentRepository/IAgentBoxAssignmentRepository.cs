using BookingSundorbon.Views.DTOs.AgentBoxAssignmentView;
using BookingSundorbon.Views.DTOs.AgentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AgentBoxAssignmentRepository
{
    public interface IAgentBoxAssignmentRepository
    {
        Task CreateAgentBoxAssignmentAsync(AgentBoxAssignmentView agentBoxAssignment);
        Task<IEnumerable<AgentBoxAssignmentView>> GetAllAgentBoxAssignment();
    }

}
