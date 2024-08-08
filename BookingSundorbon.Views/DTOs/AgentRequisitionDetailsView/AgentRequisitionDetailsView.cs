using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.AgentRequisitionDetailsView
{
    public class AgentRequisitionDetailsView
    {
        public int RequisitionNo { get; set; }
        public DateTime RequisitionDate { get; set; }
        public int RequestedQty { get; set; }
        public int DimensionId { get; set; }

    }
}
