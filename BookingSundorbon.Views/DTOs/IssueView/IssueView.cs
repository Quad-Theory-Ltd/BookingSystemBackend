using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.IssueView
{
    public class IssueView
    {
        public int IssueNo { get; set; }
        public DateTime IssueDate { get; set; }
        public int AgentRequisitionNo { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }

        public int IssuedBy { get; set; }
        public decimal IssuedPrice { get; set; }
        public string Remarks { get; set; }
        public int DimensionId { get; set; }
    }
}
