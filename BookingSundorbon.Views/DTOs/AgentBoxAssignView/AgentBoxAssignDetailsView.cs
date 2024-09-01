using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.AgentBoxAssignView
{
    public class AgentBoxAssignDetailsView
    {

        public string AgentName { get; set; }
        public string BoxName { get; set; }
        public int BoxAssignPreviousMonth { get; set; }
        public int BoxAssignCurrentMonth { get; set; }
        public int LimitPreviousMonth { get; set; }
        public int LimitCurrentMonth { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal IssueQty { get; set; }
    }
}
