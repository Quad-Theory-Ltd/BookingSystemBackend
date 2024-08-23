using BookingSundorbon.Views.DTOs.TransportAgentCostView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.TransportAgentCostRepository
{
    public interface ITransportAgentCostRepository
    {
        Task<int> CreateTransportAgentCostAsync(TransportAgentCostView transportAgentCost);
        Task<TransportAgentCostView> GetTransportAgentCostAsync(int id);
        Task<IEnumerable<TransportAgentCostView>> GetAllTransportAgentCostsAsync();
        Task UpdateTransportAgentCostAsync(TransportAgentCostView transportAgentCost);
        Task<decimal> GetCostByTransportAgentIdAsync(int transportAgentId);
        //Task DeleteTransportAgentCostAsync(int id);
    }
}
