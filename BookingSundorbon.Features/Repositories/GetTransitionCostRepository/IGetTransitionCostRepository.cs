﻿using BookingSundorbon.Views.DTOs.GetTransitionCostView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.GetTransitionCostRepository
{
    public interface IGetTransitionCostRepository
    {
        
        Task<IEnumerable<GetTransitionCostOutputView>> GetTransitionCost(GetTransitionCostView transitionCostView);
    }
}
