using BookingSundorbon.Views.DTOs.TransportAgentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.TransportAgentRepository
{
    public interface ITransportAgentRepository
    {
        Task<int> CreateTransportAgentAsync(TransportAgentView transportAgent);
        Task<TransportAgentView> GetTransportAgentAsync(int id);
        Task<IEnumerable<TransportAgentView>> GetAllActiveTransportAgentsAsync();
        Task UpdateTransportAgentAsync(TransportAgentView transportAgent);
        //Task DeleteTransportAgentAsync(int id);
    }
}
