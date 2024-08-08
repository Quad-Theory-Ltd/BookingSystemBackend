using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.AgentRequisitionView
{
    public class AgentRequisitionView
    {
        public int RequisitionNo { get; set; }
        public DateTime RequisitionDate { get; set; }
        public int AgentId { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
