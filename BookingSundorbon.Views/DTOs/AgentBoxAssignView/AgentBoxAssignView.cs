using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.AgentBoxAssignView
{
    public class AgentBoxAssignView
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int DimensionId { get; set; }
        public string BoxSerialNo { get; set; }
        public int BoxQty { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
        public string AgentName { get; set; }
        public string BoxName { get; set; }
    }
}
