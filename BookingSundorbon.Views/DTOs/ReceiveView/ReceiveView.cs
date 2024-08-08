using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ReceiveView
{
    public class ReceiveView
    {
        public int ReceiveNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int IssueNo { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ReceivedBy { get; set; }
        public Decimal ReceivedPrice { get; set; }
    }
}
