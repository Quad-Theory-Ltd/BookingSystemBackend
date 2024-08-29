using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.BoxCurrentStockView
{
    public class BoxCurrentStockView
    {
        public int Id { get; set; }
        public int DimensionId { get; set; }
        public string BoxName { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int SubbranchId { get; set; }
        public string SubbranchName { get; set; }
        public int DamageQty { get; set; }
        public int CurrentStockQty { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
