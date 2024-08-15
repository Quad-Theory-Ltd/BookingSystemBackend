﻿using BookingSundorbon.Views.DTOs.AgentBoxAssignView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AgentBoxAssignRepository
{
    public interface IAgentBoxAssignRepository
    {
        Task<CountAgentBoxAssign> CountAgentBoxAssignByAgentIdAsync(int id);
    }
}
