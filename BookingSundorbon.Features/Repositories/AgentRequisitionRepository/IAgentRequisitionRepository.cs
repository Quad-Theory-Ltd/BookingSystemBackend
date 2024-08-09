using BookingSundorbon.Views.DTOs.AgentRequisitionView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AgentRequisitionRepository
{
    public interface IAgentRequisitionRepository
    {
        Task CreateAgentRequisitionAsync(AgentRequisitionView agentRequisition);
        Task<AgentRequisitionView> GetAgentRequisitionAsync(int requisitionNo);

        Task<IEnumerable<AgentRequisitionView>> GetAllAgentRequisitionWithAgentInfoAsync();

        Task<IEnumerable<AgentRequisitionView>> GetAllAgentRequisitionAsync();

        Task<IEnumerable<int>> GetAllAgentRequisitionNo();


    }
}
