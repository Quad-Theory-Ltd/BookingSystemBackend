using BookingSundorbon.Views.DTOs.GetTransitionCostView;
using BookingSundorbon.Views.DTOs.TransitionCostView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.GetTransitionCostRepository
{
    public interface ITransitionCostRepository
    {
        Task<CreateParcelBookingOutputView> CreateParcelBookingAsync(CreateParcelBookingView createParcelBookingView);
        Task<IEnumerable<GetTransitionCostOutputView>> GetTransitionCost(GetTransitionCostView transitionCostView);
    }
}
