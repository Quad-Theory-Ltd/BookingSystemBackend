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
        //Task<AgentRequisitionView> GetAgentRequisitionAsync(int id);
        //Task<IEnumerable<AgentRequisitionView>> GetAllAgentRequisitionAsync();
 

    }
}
