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
        Task<List<CreateParcelBookingOutputView>> CreateParcelBookingAsync(List<CreateParcelBookingView> createParcelBookingViews);
        Task<IEnumerable<GetTransitionCostOutputView>> GetTransitionCost(GetTransitionCostView transitionCostView);
    }
}
