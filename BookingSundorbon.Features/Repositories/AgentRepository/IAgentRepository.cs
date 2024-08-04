using BookingSundorbon.Views.DTOs.AgentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AgentRepository
{
    public interface IAgentRepository
    {
        Task CreateAgentAsync(AgentView agent);
        Task<AgentView> GetAgentAsync(int id);
        Task<IEnumerable<AgentView>> GetAllActiveAgentAsync();
        Task UpdateAgentAsync(AgentView agent);
        Task DeleteAgentAsync(int id);
    }
}
