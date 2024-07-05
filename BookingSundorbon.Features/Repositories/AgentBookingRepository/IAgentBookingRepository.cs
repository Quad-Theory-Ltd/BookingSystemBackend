using BookingSundorbon.Views.DTOs.AgentBookingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AgentBookingRepository
{
    public interface IAgentBookingRepository
    {
        Task<IEnumerable<AgentBookingCountByDimensionView>> GetAgentBookingCountsByDimensionAsync(string id);
    }
}
