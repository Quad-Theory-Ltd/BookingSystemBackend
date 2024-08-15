using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.AgentBoxAssignView
{
    public class CountAgentBoxAssign
    {
        public int AgentId { get; set; }
        public int TotalBoxCount { get; set; }
        public int ThisMonthBoxCount { get; set; }
        public int PreviousMonthBoxCount { get; set; }
        public int LastTwoMonthCost { get; set; }
        public int Limit { get; set; }
    }
}
