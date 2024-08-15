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
        public string AgentName { get; set; }
        public int DimensionId { get; set; }
        public string BoxName { get; set; }
        public int CountBoxAssignPreviousMounth { get; set; }
        public int CountBoxAssignCurrentMounth { get; set; }
        public decimal DimensionCost { get; set; }
    }
}
